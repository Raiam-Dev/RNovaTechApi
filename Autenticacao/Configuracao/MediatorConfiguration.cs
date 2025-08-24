using Aplicacao.Entities.TarefaCommandHandler;

namespace RNovaTech.Configuracao
{
    public static class MediatorConfiguration
    {
        public static WebApplicationBuilder MediatorConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(config => {
                config.RegisterServicesFromAssembly(
                    typeof(TarefaCommandHandler).Assembly);
            });

            return builder;
        }
    }
}
