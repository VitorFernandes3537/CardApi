using CardApi.Domain;

namespace CardApi.Dtos;

public record CriarCartaoResquest(decimal Limite, Bandeira Bandeira, string Final);

public record CartaoResponse(int Id, string Final, string Bandeira,
                        decimal Limite, decimal LimiteDisponivel, bool Bloqueado);