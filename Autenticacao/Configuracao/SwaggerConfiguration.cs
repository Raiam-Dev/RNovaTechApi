using Microsoft.OpenApi.Models;

namespace RNovaTech.Configuracao
{
    public static class SwaggerConfiguration
    {
        public static WebApplicationBuilder SwaggerConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(config =>
            {
                config.EnableAnnotations();
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API RNovaTech",
                    Version = "v1",
                    Description = "API para gerenciamento de tarefas com autenticação via Firebase."
                });
            });
            return builder;
        }
    }
}
