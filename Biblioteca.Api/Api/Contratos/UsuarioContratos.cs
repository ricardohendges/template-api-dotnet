using Biblioteca.Dominio;

namespace Biblioteca.Api.Contratos;

public record UsuarioRequest(string Nome, string Email);

public record UsuarioResponse(int Id, string Nome, string Email)
{
    public static UsuarioResponse De(Usuario usuario) =>
        new(usuario.Id, usuario.Nome, usuario.Email);
}
