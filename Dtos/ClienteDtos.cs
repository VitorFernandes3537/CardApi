namespace CardApi.Dtos;


public record CriarClienteRequest(string Nome, string Cpf);

public record CartaoResponse(int Id, string Final, string Bandeira, 
                        decimal Limite, decimal LimiteDisponivel, bool Bloqueado);
public record ClienteResponse(int Id, string Nome, string Cpf, 
                                IReadOnlyList<CartaoResponse> Cartoes);