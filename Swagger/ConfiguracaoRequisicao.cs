using GestaoDeAplicacoesApi.Util;

namespace GestaoDeAplicacoesApi.Swagger
{

    public class ConfiguracaoRequisicaoBloqueaMiddleware
    {
        private readonly RequestDelegate _next;

        public ConfiguracaoRequisicaoBloqueaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (ConfiguracaoRequisicao.LimiteDeRequisicoes())
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                throw new ExceptionCustomizado("Por excessivas requisições, são bloqueadas temporariamente as chamadas. Volte a tentar em alguns segundos!", "ER-108");
                //await context.Response.WriteAsync("Por excessivas requisições, são bloqueadas temporariamente as chamadas. Volte a tentar em alguns segundos.");
                //return;
            }

            ConfiguracaoRequisicao.IncrementarRequisicao();
            await _next(context);
        }

        private static class ConfiguracaoRequisicao
        {
            public static int QuantidadeRequisicao = 0;
            public static readonly int LimiteRequisicoes = 3; // Exemplo de limite de 100 requisições
            public static readonly TimeSpan Intervalo = TimeSpan.FromSeconds(60); // Intervalo de 1 minuto
            private static DateTime UltimaAtualizacao = DateTime.UtcNow;

            public static void IncrementarRequisicao()
            {
                var agora = DateTime.UtcNow;
                if ((agora - UltimaAtualizacao) > Intervalo)
                {
                    // Resetar contador se o intervalo tiver passado
                    QuantidadeRequisicao = 0;
                    UltimaAtualizacao = agora;
                }

                QuantidadeRequisicao++;
            }

            public static bool LimiteDeRequisicoes()
            {
                return QuantidadeRequisicao >= LimiteRequisicoes;
            }
        }

    }

}
