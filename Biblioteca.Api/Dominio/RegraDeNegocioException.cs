namespace Biblioteca.Dominio;

public class RegraDeNegocioException : Exception
{
    public RegraDeNegocioException(string mensagem) : base(mensagem)
    {
    }
}
