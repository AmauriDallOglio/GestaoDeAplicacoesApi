
using GestaoDeAplicacoesApi.Swagger;

namespace GestaoDeAplicacoesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AdicionaClassesSingleton();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Api
            builder.Services.InformacaoCabecalhoApi();
            builder.Services.VersionamentoApi();
            //  builder.Services.BotaoAutorizacaoToken();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.ConfiguracaoSwagger();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseMiddleware<MiddlewareError>();

            app.MapControllers();

            app.Run();
        }
    }
}
