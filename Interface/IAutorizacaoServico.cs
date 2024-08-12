namespace GestaoDeAplicacoesApi.Interface
{
    public interface IAutorizacaoServico
    {
        bool RetornaIdUsuario(string usuario, string senha, out string usuarioId);

    }
}
