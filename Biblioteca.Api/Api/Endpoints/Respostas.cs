using Biblioteca.Aplicacao;
using Biblioteca.Dominio;

namespace Biblioteca.Api.Endpoints;

// Exceção -> status HTTP, num lugar só.
public static class Respostas
{
    public static IResult Executar(Func<IResult> acao)
    {
        try
        {
            return acao();
        }
        catch (NaoEncontradoException ex)
        {
            return Results.NotFound(new { mensagem = ex.Message });
        }
        catch (RegraDeNegocioException ex)
        {
            return Results.BadRequest(new { mensagem = ex.Message });
        }
    }
}
