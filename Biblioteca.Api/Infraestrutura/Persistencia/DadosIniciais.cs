using Biblioteca.Dominio;

namespace Biblioteca.Infraestrutura.Persistencia;

public static class DadosIniciais
{
    public static RepositorioMemoria<Livro> Livros()
    {
        var repositorio = new RepositorioMemoria<Livro>();
        repositorio.Adicionar(new Livro("Clean Code", "Martin", 2008));
        repositorio.Adicionar(new Livro("O Programador Pragmático", "Hunt", 1999));
        return repositorio;
    }

    public static RepositorioMemoria<Usuario> Usuarios()
    {
        var repositorio = new RepositorioMemoria<Usuario>();
        repositorio.Adicionar(new Usuario("Ana Souza", "ana@example.com"));
        repositorio.Adicionar(new Usuario("Bruno Lima", "bruno@example.com"));
        return repositorio;
    }
}
