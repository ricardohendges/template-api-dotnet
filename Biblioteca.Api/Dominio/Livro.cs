namespace Biblioteca.Dominio;

public class Livro : IIdentificavel
{
    public int Id { get; set; }
    public string Titulo { get; private set; } = "";
    public string Autor { get; private set; } = "";
    public int Ano { get; private set; }
    public bool Disponivel { get; private set; }

    public Livro(string titulo, string autor, int ano, bool disponivel = true)
        => AlterarDados(titulo, autor, ano, disponivel);

    public void AlterarDados(string titulo, string autor, int ano, bool disponivel)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new RegraDeNegocioException("Título é obrigatório.");
        if (string.IsNullOrWhiteSpace(autor))
            throw new RegraDeNegocioException("Autor é obrigatório.");
        if (ano < 1450 || ano > DateTime.Now.Year)
            throw new RegraDeNegocioException("Ano inválido.");

        Titulo = titulo;
        Autor = autor;
        Ano = ano;
        Disponivel = disponivel;
    }
}
