using Biblioteca.Aplicacao.Livros;
using Biblioteca.Aplicacao.Usuarios;

namespace Biblioteca.Aplicacao;

public static class AplicacaoExtensions
{
    public static IServiceCollection AddAplicacao(this IServiceCollection services)
    {
        services.AddScoped<LivroServico>();
        services.AddScoped<UsuarioServico>();
        return services;
    }
}
