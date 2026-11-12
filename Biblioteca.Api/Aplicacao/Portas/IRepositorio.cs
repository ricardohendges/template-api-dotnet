using Biblioteca.Dominio;

namespace Biblioteca.Aplicacao.Portas;

public interface IRepositorio<T> where T : IIdentificavel
{
    void Adicionar(T item);
    T? BuscarPorId(int id);
    IEnumerable<T> Listar();
    void Atualizar(T item);
    bool Remover(int id);
}
