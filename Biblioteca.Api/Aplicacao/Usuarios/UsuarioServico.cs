using Biblioteca.Aplicacao.Portas;
using Biblioteca.Dominio;

namespace Biblioteca.Aplicacao.Usuarios;

public class UsuarioServico
{
    private readonly IRepositorio<Usuario> _usuarios;

    public UsuarioServico(IRepositorio<Usuario> usuarios) => _usuarios = usuarios;

    public IEnumerable<Usuario> Listar() => _usuarios.Listar();

    public Usuario Buscar(int id) =>
        _usuarios.BuscarPorId(id)
            ?? throw new NaoEncontradoException($"Usuario {id} não encontrado.");

    public Usuario Cadastrar(string nome, string email)
    {
        GarantirEmailDisponivel(email, idAtual: null);
        var usuario = new Usuario(nome, email);
        _usuarios.Adicionar(usuario);
        return usuario;
    }

    public Usuario Alterar(int id, string nome, string email)
    {
        var usuario = Buscar(id);
        GarantirEmailDisponivel(email, idAtual: id);
        usuario.AlterarDados(nome, email);
        _usuarios.Atualizar(usuario);
        return usuario;
    }

    public void Remover(int id)
    {
        if (!_usuarios.Remover(id))
            throw new NaoEncontradoException($"Usuario {id} não encontrado.");
    }

    // Um usuário sozinho não sabe dos outros: quem garante a unicidade é o serviço, que tem o repositório.
    private void GarantirEmailDisponivel(string email, int? idAtual)
    {
        var existente = _usuarios.Listar()
            .FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (existente is not null && existente.Id != idAtual)
            throw new RegraDeNegocioException($"E-mail '{email}' já cadastrado.");
    }
}
