using Biblioteca.Api.Contratos;
using Biblioteca.Aplicacao.Livros;

namespace Biblioteca.Api.Endpoints;

public static class LivrosEndpoints
{
    public static void MapLivrosEndpoints(this WebApplication app)
    {
        var grupo = app.MapGroup("/livros");

        // GET /livros              -> todos
        // GET /livros?autor=martin -> só os do autor
        grupo.MapGet("/", (string? autor, LivroServico servico) =>
            Results.Ok(servico.Listar(autor).Select(LivroResponse.De)));

        grupo.MapGet("/{id:int}", (int id, LivroServico servico) =>
            Respostas.Executar(() =>
                Results.Ok(LivroResponse.De(servico.Buscar(id)))));

        grupo.MapPost("/", (LivroRequest req, LivroServico servico) =>
            Respostas.Executar(() =>
            {
                var livro = servico.Cadastrar(req.Titulo, req.Autor, req.Ano, req.Disponivel);
                return Results.Created($"/livros/{livro.Id}", LivroResponse.De(livro));
            }));

        grupo.MapPut("/{id:int}", (int id, LivroRequest req, LivroServico servico) =>
            Respostas.Executar(() =>
                Results.Ok(LivroResponse.De(servico.Alterar(id, req.Titulo, req.Autor, req.Ano, req.Disponivel)))));

        // Mandar o mesmo DELETE duas vezes: a primeira devolve 204, a segunda 404.
        grupo.MapDelete("/{id:int}", (int id, LivroServico servico) =>
            Respostas.Executar(() =>
            {
                servico.Remover(id);
                return Results.NoContent();
            }));
    }
}
