namespace RNovaTech.Configuracao
{
    public static class ControllerConfiguration
    {
        public static WebApplicationBuilder ControllerConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add
                    (
                        new System.Text.Json.Serialization
                            .JsonStringEnumConverter()
                    );
                });

            return builder;
        }
    }
}
