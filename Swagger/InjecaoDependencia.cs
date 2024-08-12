using GestaoDeAplicacoesApi.Interface;
using GestaoDeAplicacoesApi.Servico;

namespace GestaoDeAplicacoesApi.Swagger
{
    public static class InjecaoDependencia
    {
        /// <summary>
        /// Gerenciamento de Ciclo de Vida: O uso de Singleton garante que haja apenas uma instância da classe em toda a aplicação.
        /// </summary>
        /// <param name="services"></param>
        public static void AdicionaClassesSingleton(this IServiceCollection services)
        {
            services.AddSingleton<IAutorizacaoServico, AutorizacaoServico>();
            services.AddSingleton<ITokenServico, TokenServico>();
        }
    }
}
