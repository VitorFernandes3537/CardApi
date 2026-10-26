namespace CardApi.Dtos;


public record CriarClienteRequest(string Nome, string Cpf);


public record ClienteResponse(int Id, string Nome, string Cpf, 
                                IReadOnlyList<CartaoResponse> Cartoes);