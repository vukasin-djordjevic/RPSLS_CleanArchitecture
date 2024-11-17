using Application;
using Infrastructure;
using Persistence;
using Presentation;
using Serilog;
using WebApi.Extensions;
using WebApi.Infrastructure;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services
    .Scan(
        selector => selector
            .FromAssemblies(
                Infrastructure.AssemblyReference.Assembly,
                Persistence.AssemblyReference.Assembly)
            .AddClasses(false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());


builder
    .Services
    .AddControllers()
    .AddApplicationPart(Presentation.AssemblyReference.Assembly);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddPresentation()
    .AddPersistence(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program { }
