namespace CardApi.Domain;

public class Cliente
{
    private const int LimiteDeCartoes = 3;
    public int Id { get; private set; }

    public string Nome { get; private set; } = string.Empty;

    public string Cpf { get; private set; } = string.Empty;

    private readonly List<Cartao> _cartoes = [];

    public IReadOnlyCollection<Cartao> Cartoes => _cartoes;

    private Cliente() { }

    public Cliente(string nome, string cpf)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nome);
        ArgumentException.ThrowIfNullOrWhiteSpace(cpf);

        string cpfLimpo = cpf.Replace(".", "").Replace("-", "");

        ArgumentOutOfRangeException.ThrowIfNotEqual(cpfLimpo.Length, 11);

        if (!cpfLimpo.All(char.IsAsciiDigit))
            throw new ArgumentException("O Cpf deve conter apenas dígitos numéricos", nameof(cpf));

        Nome = nome;
        Cpf = cpfLimpo;
    }

    public Cartao AdicionarCartao(decimal limite, Bandeira bandeira, string final)
    {
        if (_cartoes.Count >= LimiteDeCartoes)
            throw new InvalidOperationException($"O Cliente pode ter no máximo {LimiteDeCartoes} cartões.");

        Cartao novoCartao = new Cartao(limite, bandeira, final);

        _cartoes.Add(novoCartao);

        return novoCartao;
    }
}