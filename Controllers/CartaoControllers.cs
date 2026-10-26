using CardApi.Data;
using CardApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardApi.Controllers;


[Route("cartoes")]
[ApiController]
public class CartaoController(AppDbContext db) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CartaoResponse>> BuscarCartaoPorId(int id, CancellationToken ct)
    {
        var cartao = await db.Cartoes.FirstOrDefaultAsync(c => c.Id == id, ct);
        
        if(cartao is null) return NotFound();

        return cartao.ToResponse();
    }
}