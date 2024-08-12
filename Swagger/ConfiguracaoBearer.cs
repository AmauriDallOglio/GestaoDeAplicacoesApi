using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace GestaoDeAplicacoesApi.Swagger
{
    internal static class ConfiguracaoBearer
    {
        public static void VersionamentoApi(this IServiceCollection services)
        {


            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true; //  a versão padrão (1.0 neste caso) será usada automaticamente.
                options.DefaultApiVersion = new ApiVersion(1, 0); // Se a versão não for especificada na solicitação, esta será a versão usada.
                options.ReportApiVersions = true; // Versões da API devem ser relatadas nas respostas.
            });
        }

        public static void InformacaoCabecalhoApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                //c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Gerenciador de autorização.",
                    Description = "O processo de autenticação é utilizado para verificar a identidade de uma pessoa em função de um ou vários fatores, garantindo que os dados de quem os enviou sejam corretos.",
                    TermsOfService = new Uri("https://google.com.br/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Amauri",
                        Email = "amauri@hotmail.com",
                        Url = new Uri("https://google.com.br/")
                    }
                });
            });
        }



        internal static void ConfiguracaoSwagger(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", typeof(Program).Assembly.GetName().Name);

                // Adiciona o botão "Authorize" ao Swagger UI
                options.DocExpansion(DocExpansion.List);
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
            });
        }
    }
}
