using GatewayService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddYarp();

builder.Services.AddTokenVerification();

builder.Services.AddCustomCors();

var app = builder.Build();

app.UseCors();

app.MapReverseProxy();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
