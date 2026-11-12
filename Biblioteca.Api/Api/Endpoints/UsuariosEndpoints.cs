using Biblioteca.Api.Contratos;
using Biblioteca.Aplicacao.Usuarios;

namespace Biblioteca.Api.Endpoints;

public static class UsuariosEndpoints
{
    public static void MapUsuariosEndpoints(this WebApplication app)
    {
        var grupo = app.MapGroup("/usuarios");

        grupo.MapGet("/", (UsuarioServico servico) =>
            Results.Ok(servico.Listar().Select(UsuarioResponse.De)));

        grupo.MapGet("/{id:int}", (int id, UsuarioServico servico) =>
            Respostas.Executar(() =>
                Results.Ok(UsuarioResponse.De(servico.Buscar(id)))));

        grupo.MapPost("/", (UsuarioRequest req, UsuarioServico servico) =>
            Respostas.Executar(() =>
            {
                var usuario = servico.Cadastrar(req.Nome, req.Email);
                return Results.Created($"/usuarios/{usuario.Id}", UsuarioResponse.De(usuario));
            }));

        grupo.MapPut("/{id:int}", (int id, UsuarioRequest req, UsuarioServico servico) =>
            Respostas.Executar(() =>
                Results.Ok(UsuarioResponse.De(servico.Alterar(id, req.Nome, req.Email)))));

        grupo.MapDelete("/{id:int}", (int id, UsuarioServico servico) =>
            Respostas.Executar(() =>
            {
                servico.Remover(id);
                return Results.NoContent();
            }));
    }
}
