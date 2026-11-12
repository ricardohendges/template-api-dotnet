namespace Biblioteca.Dominio;

public class Usuario : IIdentificavel
{
    public int Id { get; set; }
    public string Nome { get; private set; } = "";
    public string Email { get; private set; } = "";

    public Usuario(string nome, string email)
        => AlterarDados(nome, email);

    public void AlterarDados(string nome, string email)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new RegraDeNegocioException("Nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new RegraDeNegocioException("E-mail inválido.");

        Nome = nome;
        Email = email;
    }
}
