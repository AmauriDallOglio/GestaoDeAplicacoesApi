using GestaoDeAplicacoesApi.DTO;
using GestaoDeAplicacoesApi.Interface;
using Microsoft.AspNetCore.Mvc;
using static GestaoDeAplicacoesApi.Swagger.MiddlewareError;

namespace GestaoDeAplicacoesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorizacaoController : Controller
    {
        private readonly IAutorizacaoServico _iLoginServico;
        private readonly ITokenServico _iTokenServico;
        private readonly IConfiguration _configuration;

        public AutorizacaoController(IAutorizacaoServico iLoginServico, ITokenServico iTokenServico, IConfiguration configuration)
        {
            _iLoginServico = iLoginServico;
            _iTokenServico = iTokenServico;
            _configuration = configuration;

        }

        [HttpPost("GerarToken")]
        public IActionResult GerarToken([FromBody] LoginDto model)
        {
      
            if (_iLoginServico.RetornaIdUsuario(model.Usuario, model.Senha, out var usuarioId))
            {
                var token = _iTokenServico.GerarToken(usuarioId, _configuration);
                return Ok(new { Token = token });
            }
            if (model == null)
            {
                throw new InvalidGrantException("Usuário ou senha inválida!");
            }
            return Unauthorized("Usuário ou senha inválida!");
        }


    }
}
