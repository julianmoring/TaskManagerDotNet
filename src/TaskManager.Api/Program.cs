using JasperFx.CodeGeneration.Model;
using JasperFx.RuntimeCompiler;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Extensions;
using TaskManager.Infrastructure.Persistence;
using Wolverine;
using Wolverine.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTaskManagerInfrastructure(builder.Configuration);
builder.Host.UseWolverine(opts =>
{
    opts.ServiceLocationPolicy = ServiceLocationPolicy.AlwaysAllowed;
});
builder.Services.AddRuntimeCompilation();
builder.Services.AddWolverineHttp();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TaskManagerDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.MapWolverineEndpoints(opts => opts.WarmUpRoutes = RouteWarmup.Eager);

app.MapGet("/api/health", () => Results.Ok(new { status = "ok" }));

app.MapFallbackToFile("index.html");

app.Run();

public partial class Program { }
