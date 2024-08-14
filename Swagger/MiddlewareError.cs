using GestaoDeAplicacoesApi.Util;
using Newtonsoft.Json;
using System.Net;

namespace GestaoDeAplicacoesApi.Swagger
{
    public class MiddlewareError
    {
        private readonly RequestDelegate _next;

        public MiddlewareError(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string error = "";
            string errorDescription = "";


            if (exception is ExceptionCustomizado exceptionCustomizado)
            {
                statusCode = HttpStatusCode.BadRequest;
                error = exceptionCustomizado.Codigo;
                errorDescription = exceptionCustomizado.Message;
            }


  

            //if (exception is InvalidGrantException)
            //{
            //    statusCode = HttpStatusCode.BadRequest;
            //    error = "invalid_grant";
            //    errorDescription = "Os motivos são vários, pode ser porque o authorization_code ou refresh_token são inválidos, expiraram ou foram revogados, foram enviados em um fluxo incorreto, pertencem a outro cliente ou o redirect_uri usado no fluxo de autorização não corresponde ao que tem configurado seu aplicativo."; //"Error validating grant. Your authorization code or refresh token may be expired or it was already used.";
            //}
            //else if (exception is InvalidClientException)
            //{
            //    statusCode = HttpStatusCode.BadRequest;
            //    error = "invalid_client";
            //    // errorDescription = "The client_id and/or client_secret provided are invalid.";
            //    errorDescription = "O client_id e/ou client_secret do seu aplicativo fornecido é inválido.";
            //}
            //else if (exception is InvalidScopeException)
            //{
            //    statusCode = HttpStatusCode.BadRequest;
            //    error = "invalid_scope";
            //    errorDescription = "O alcance solicitado é inválido, desconhecido ou foi criado no formato errado. Os valores permitidos para o parâmetro alcance são: ‘offline_access’, ‘write’ e ‘read’."; // "The requested scope is invalid, unknown, or malformed.";
            //}
            //else if (exception is InvalidRequestException)
            //{
            //    statusCode = HttpStatusCode.BadRequest;
            //    error = "invalid_request";
            //    errorDescription = "A solicitação não inclui um parâmetro obrigatório, inclui um parâmetro ou valor de parâmetro não suportado, tem algum valor dobrado ou está mal formado."; //"The request is missing a required parameter, includes an unsupported parameter or value, has a duplicated parameter, or is malformed.";
            //}
            //else if (exception is UnsupportedGrantTypeException)
            //{
            //    statusCode = HttpStatusCode.BadRequest;
            //    error = "unsupported_grant_type";
            //    errorDescription = "Os valores permitidos para grant_type são ‘authorization_code’ ou ‘refresh_token’."; // "The provided grant_type is not supported.";
            //}
            //else if (exception is ForbiddenException)
            //{
            //    statusCode = HttpStatusCode.Forbidden;
            //    error = "forbidden";
            //    errorDescription = "A chamada não autoriza o acesso, possivelmente está sendo usado o token de outro usuário, ou para o caso de grant o usuário não tem acesso à URL de Mercado Livre de seu país (.ar, .br, .mx, etc) e deve verificar que sua conexão ou navegador funcione corretamente para os domínios do MELI.";// "The request is not authorized. Ensure you are using the correct user token or check access permissions.";
            //}
            //else if (exception is LocalRateLimitedException)
            //{
            //    statusCode = HttpStatusCode.TooManyRequests;
            //    error = "local_rate_limited";
            //    errorDescription = "Por excessivas requisições, são bloqueadas temporariamente as chamadas. Volte a tentar em alguns segundos."; // "Too many requests. Please try again later.";
            //}
            //else if (exception is UnauthorizedClientException)
            //{
            //    statusCode = HttpStatusCode.Unauthorized;
            //    error = "unauthorized_client";
            //    errorDescription = "A aplicação não tem grant com o usuário ou as permissões (scopes) que tem o aplicativo com esse usuário. Não permitem criar um token."; // "The application does not have the necessary grants with the user.";
            //}
            //else if (exception is UnauthorizedApplicationException)
            //{
            //    statusCode = HttpStatusCode.Unauthorized;
            //    error = "unauthorized_application";
            //    errorDescription = "A aplicação está bloqueada, e por isso não poderá operar até resolver o problema."; // "The application is blocked and cannot operate until the issue is resolved.";
            //}

            var response = new
            {
                error,
                statusCode,
                error_description = errorDescription
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }


        //public class ExceptionGenerico : ExceptionCustomizado
        //{
        //    public ExceptionGenerico(string message, string codigo, Exception inner = null) : base(message, codigo, inner)
        //    {
        //    }
        //}



    }


}