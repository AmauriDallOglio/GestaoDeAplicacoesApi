using System.Text;
using static GestaoDeAplicacoesApi.Swagger.MiddlewareError;

namespace GestaoDeAplicacoesApi.Swagger
{
    public static class ConfiguracaoAppSettings
    {
        public static string RetornaSecret(IConfiguration configuration)
        {
            var secret = configuration["Jwt:Secret"];

            if (secret.Length < 32)
            {
                throw new InvalidGrantException("Teste");
                throw new ArgumentException("A chave secreta não localizada.");
            }
            return secret;
        }

        public static byte[] RetornaSecretASCII(string secret)
        {
            var array = Encoding.ASCII.GetBytes(secret);
            if (array.Length < 32)
            {
                throw new ArgumentException("A chave secreta deve ter pelo menos 256 bits (32 caracteres) para o algoritmo HMAC-SHA256.");
            }
            return array;
        }

    }


}
