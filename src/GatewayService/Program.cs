using GatewayService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddYarp();

builder.Services.AddTokenVerification();

var app = builder.Build();

app.MapReverseProxy();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
