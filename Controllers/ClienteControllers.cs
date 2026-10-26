using CardApi.Data;
using CardApi.Domain;
using CardApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardApi.Controllers;

[Route("clientes")]
[ApiController]
public class ClientesController(AppDbContext db) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ClienteResponse>> Criar(CriarClienteRequest request, CancellationToken ct)
    {
        var cliente = new Cliente(request.Nome, request.Cpf);
        db.Clientes.Add(cliente);
        await db.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(BuscarPorId), new { id = cliente.Id }, cliente.ToResponse());
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClienteResponse>> BuscarPorId(int id, CancellationToken ct)
    {
        var cliente = await db.Clientes.Include(c => c.Cartoes).FirstOrDefaultAsync(c => c.Id == id, ct);

        if (cliente is null) return NotFound();

        return cliente.ToResponse();

    }

    [HttpPost("{id:int}/cartoes")]
    public async Task<ActionResult<CartaoResponse>> Criar(CriarCartaoResquest request, int id, CancellationToken ct)
    {
        var cliente = await db.Clientes.Include(c => c.Cartoes).FirstOrDefaultAsync(c => c.Id == id, ct);
        if (cliente is null)
            return NotFound();

        var cartao = cliente.AdicionarCartao(request.Limite, request.Bandeira, request.Final);
        await db.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(CartaoController.BuscarCartaoPorId), nameof(CartaoController).Replace("Controller", ""), new { id = cartao.Id }, cartao.ToResponse());
    }
}