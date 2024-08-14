using GestaoDeAplicacoesApi.Interface;
using GestaoDeAplicacoesApi.Swagger;
using GestaoDeAplicacoesApi.Util;

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
            if (string.IsNullOrEmpty(usuario))
            {
 
                throw new ExceptionCustomizado("Usuário inválido ou sem autorização!", "ER-100");
            }

            if (string.IsNullOrEmpty(senha))
            {
                 throw new ExceptionCustomizado("Senha inválido ou sem autorização!", "ER-102");
            }

            
            if (usuarios.TryGetValue(usuario, out var storedPassword) && storedPassword == senha)
            {
                usuarioId = Guid.NewGuid().ToString();
                return true;
            }

            if (usuarioId == null)
            {
                throw new ExceptionCustomizado("Usuário ou senha inválida!", "ER-107");
            }

            return false;
        }



    }
}
