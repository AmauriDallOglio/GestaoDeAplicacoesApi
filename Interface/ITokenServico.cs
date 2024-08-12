namespace GestaoDeAplicacoesApi.Interface
{
    public interface ITokenServico
    {
        string GerarToken(string usuarioId, IConfiguration configuration);
    }
}
