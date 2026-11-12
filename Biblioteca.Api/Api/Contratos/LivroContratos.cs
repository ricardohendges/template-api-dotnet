using Biblioteca.Dominio;

namespace Biblioteca.Api.Contratos;

// O que ENTRA: o cliente não manda Id
public record LivroRequest(string Titulo, string Autor, int Ano, bool Disponivel = true);

// O que SAI
public record LivroResponse(int Id, string Titulo, string Autor, int Ano, bool Disponivel)
{
    public static LivroResponse De(Livro livro) =>
        new(livro.Id, livro.Titulo, livro.Autor, livro.Ano, livro.Disponivel);
}
