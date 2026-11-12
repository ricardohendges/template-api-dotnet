namespace Biblioteca.Aplicacao;

public class NaoEncontradoException : Exception
{
    public NaoEncontradoException(string mensagem) : base(mensagem)
    {
    }
}
