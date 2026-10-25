namespace CardApi.Domain;


public class Cartao
{
    public int Id { get; private set; }

    public int ClienteId { get; private set; }

    public string Final { get; private set; } = string.Empty;
    public Bandeira Bandeira { get; private set; }

    public decimal Limite { get; private set; }

    public decimal LimiteDisponivel { get; private set; }

    public bool Bloqueado { get; private set; }

    private Cartao() { }

    internal Cartao(decimal limite, Bandeira bandeira, string final)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(limite);
        ArgumentException.ThrowIfNullOrWhiteSpace(final);
        ArgumentOutOfRangeException.ThrowIfNotEqual(final.Length, 4);

        if (!final.All(char.IsAsciiDigit))
            throw new ArgumentException("O final deve conter apenas dígitos.", nameof(final));

        if (!Enum.IsDefined(bandeira))
            throw new ArgumentOutOfRangeException(nameof(bandeira), bandeira,  "A Bandeira informada não é válida.");

        Final = final;
        Bandeira = bandeira;
        Bloqueado = false;
        Limite = limite;
        LimiteDisponivel = limite;
    }

}