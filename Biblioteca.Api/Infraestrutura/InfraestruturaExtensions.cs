using Biblioteca.Aplicacao.Portas;
using Biblioteca.Dominio;
using Biblioteca.Infraestrutura.Persistencia;

namespace Biblioteca.Infraestrutura;

public static class InfraestruturaExtensions
{
    // Aqui se escolhe o ADAPTADOR. Trocar memória por SQLite = um adaptador novo e uma linha aqui.
    public static IServiceCollection AddInfraestrutura(this IServiceCollection services)
    {
        services.AddSingleton<IRepositorio<Livro>>(_ => DadosIniciais.Livros());
        services.AddSingleton<IRepositorio<Usuario>>(_ => DadosIniciais.Usuarios());
        return services;
    }
}
