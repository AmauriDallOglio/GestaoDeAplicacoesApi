using GestaoDeAplicacoesApi.Interface;

namespace GestaoDeAplicacoesApi.Servico
{
    public class AutorizacaoServico : IAutorizacaoServico
    {
        private readonly Dictionary<string, string> usuarios = new()
        {
            { "amauri", "123" },
            { "amauri2", "123" }
        };

        public bool RetornaIdUsuario(string usuario, string senha, out string usuarioId)
        {
            usuarioId = null;

            if (usuarios.TryGetValue(usuario, out var storedPassword) && storedPassword == senha)
            {
                usuarioId = Guid.NewGuid().ToString();
                return true;
            }

            return false;
        }



    }
}
