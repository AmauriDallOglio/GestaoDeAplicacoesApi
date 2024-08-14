using GestaoDeAplicacoesApi.Interface;
using GestaoDeAplicacoesApi.Swagger;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GestaoDeAplicacoesApi.Servico
{
    public class TokenServico : ITokenServico
    {
        public string GerarToken(string usuarioId, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            string secretString = ConfiguracaoAppSettings.RetornaSecret(configuration);
            byte[] secretASCII = ConfiguracaoAppSettings.RetornaSecretASCII(secretString);

 
            var key = new SymmetricSecurityKey(secretASCII);
            string nomeUsuario = "Amauri D.";
            DateTime vencimentoToken = DateTime.Now.AddHours(3);

            var claims = new[]
            {
                new Claim("NomeUsuario", nomeUsuario),
                new Claim("token_type", "bearer"),

                new Claim("UsuarioId", Guid.NewGuid().ToString()),
                new Claim("VencimentoToken", vencimentoToken.ToString(), ClaimValueTypes.Integer64),
                new Claim("DataGeracao", DateTime.Now.ToString(), ClaimValueTypes.Integer64),
                new Claim("refresh_token", GerarRefreshToken()),

            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: vencimentoToken,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature));


            return tokenHandler.WriteToken(token);
        }

        public string GerarRefreshToken()
        {
            return "RT-" + Guid.NewGuid().ToString();
        }
    }
}
