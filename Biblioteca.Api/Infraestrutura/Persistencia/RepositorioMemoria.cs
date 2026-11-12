using Biblioteca.Aplicacao.Portas;
using Biblioteca.Dominio;

namespace Biblioteca.Infraestrutura.Persistencia;

// ADAPTADOR: implementa a porta guardando tudo numa List<T>.
// Singleton: várias requisições podem chegar ao mesmo tempo, daí o lock.
public class RepositorioMemoria<T> : IRepositorio<T> where T : IIdentificavel
{
    private readonly List<T> _itens = new();
    private readonly object _lock = new();

    public void Adicionar(T item)
    {
        lock (_lock)
        {
            item.Id = _itens.Count > 0 ? _itens.Max(i => i.Id) + 1 : 1;
            _itens.Add(item);
        }
    }

    public T? BuscarPorId(int id)
    {
        lock (_lock)
            return _itens.FirstOrDefault(i => i.Id == id);
    }

    public IEnumerable<T> Listar()
    {
        lock (_lock)
            return _itens.ToList();
    }

    public void Atualizar(T item) { } // em memória, já mudou por referência

    public bool Remover(int id)
    {
        lock (_lock)
            return _itens.RemoveAll(i => i.Id == id) > 0;
    }
}
