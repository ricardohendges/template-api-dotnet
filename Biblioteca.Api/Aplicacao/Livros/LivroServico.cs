using Biblioteca.Aplicacao.Portas;
using Biblioteca.Dominio;

namespace Biblioteca.Aplicacao.Livros;

public class LivroServico
{
    private readonly IRepositorio<Livro> _livros;

    public LivroServico(IRepositorio<Livro> livros) => _livros = livros;

    public IEnumerable<Livro> Listar(string? autor)
    {
        var livros = _livros.Listar();
        if (string.IsNullOrWhiteSpace(autor))
            return livros;

        return livros.Where(l => l.Autor.Contains(autor, StringComparison.OrdinalIgnoreCase));
    }

    public Livro Buscar(int id) =>
        _livros.BuscarPorId(id)
            ?? throw new NaoEncontradoException($"Livro {id} não encontrado.");

    public Livro Cadastrar(string titulo, string autor, int ano, bool disponivel)
    {
        var livro = new Livro(titulo, autor, ano, disponivel);
        _livros.Adicionar(livro);
        return livro;
    }

    public Livro Alterar(int id, string titulo, string autor, int ano, bool disponivel)
    {
        var livro = Buscar(id);
        livro.AlterarDados(titulo, autor, ano, disponivel);
        _livros.Atualizar(livro);
        return livro;
    }

    public void Remover(int id)
    {
        if (!_livros.Remover(id))
            throw new NaoEncontradoException($"Livro {id} não encontrado.");
    }
}
