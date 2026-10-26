
using CardApi.Domain;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CardApi.Api;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken ct)
    {
        var (status, detalhe) = exception switch
        {
            ArgumentException e => (400, e.Message),
            DomainException e => (422, e.Message),
            DbUpdateException { InnerException: SqlException { Number: 2601 or 2627 } } e => (409, e.Message),
            _ => (500, "Ocorreu um erro inesperado. Informe o TraceId ao Suporte.")
        };

        var titulo = ReasonPhrases.GetReasonPhrase(status);


        // 2. log: Error com a exceção se status >= 500; senão Warning

        if (status >= 500)
            logger.LogError(exception, "Falha ao processar {Caminho}, traceId: {TraceId}", httpContext.Request.Path, httpContext.TraceIdentifier);
        else
            logger.LogWarning("Requisição recusada com {Status} em {Caminho}: {Detalhe}, traceId: {TraceId}", status, httpContext.Request.Path, detalhe, httpContext.TraceIdentifier);


        // 3. new ProblemDetails { ... } + traceId

        var problema = new ProblemDetails
        {
            Status = status,
            Title = titulo,
            Detail = detalhe
        };
        problema.Extensions["traceId"] = httpContext.TraceIdentifier;

        // 4. StatusCode + WriteAsJsonAsync
        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(problema, ct);

        return true;
    }
}