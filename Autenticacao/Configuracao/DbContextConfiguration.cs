using AplicacaoWebApi.Infra.Data;
using Microsoft.EntityFrameworkCore;
using RNovaTech.Infra.Data;
using System.Runtime.CompilerServices;

namespace RNovaTech.Configuracao
{
    public static class DbContextConfiguration
    {
        public static WebApplicationBuilder DbContextConfig(this WebApplicationBuilder builder)
        {
            //builder.Services.AddDbContext<DbContextMemory>(config => {
            //    config.UseInMemoryDatabase(
            //        builder.Configuration
            //            .GetConnectionString("DataBaseMemoria")!);
            //});
            builder.Services.AddDbContext<DbContextProducao>(options =>
            {
                options.UseNpgsql(builder.Configuration["DB_CONNECTION_STRING"]);
            });

            return builder;
        }
    }
}
