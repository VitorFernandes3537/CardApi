using CardApi.Domain;
namespace CardApi.Dtos;

public static class Mapeamentos
{
    public static CartaoResponse ToResponse(this Cartao c) => 
        new(c.Id, c.Final, c.Bandeira.ToString(), c.Limite, c.LimiteDisponivel, c.Bloqueado);

    public static ClienteResponse ToResponse(this Cliente c) => 
        new (c.Id, c.Nome, c.Cpf, c.Cartoes.Select(cartao => cartao.ToResponse()).ToList());
}