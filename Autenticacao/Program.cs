using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder.Extensions;
using RNovaTech.Configuracao;
using RNovaTech.Features._Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder
    .DbContextConfig()
    .MediatorConfig()
    .ControllerConfig()
    .SwaggerConfig();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
var env = app.Environment;

////TODO: Remover da Condicional
//if (env.IsProduction())
//{
//    var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
//    app.Urls.Add($"http://*:{port}");
//}


var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.SwaggerEndpoint("./v1/swagger.json", "API RNovaTech v1");
    config.RoutePrefix = "swagger"; 
});

app.MapControllers();

app.Run();
