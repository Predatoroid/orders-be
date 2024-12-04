using System.Data;
using System.Reflection;
using FlexERP.Shared.Extensions;
using Npgsql;
using FlexERP.WebApi.Extensions;
using Prometheus;
using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose() 
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .WriteTo.Console()
    .WriteTo.Graylog(new GraylogSinkOptions
    {
        HostnameOrAddress = "localhost",
        Port = 12201,
        Facility = "MinimalAPI",
        TransportType = TransportType.Tcp
    })
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddSingleton<IDbConnection>(sp => new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddServicesByReflection(Assembly.GetExecutingAssembly());
builder.Services.AddRepositoriesByReflection(Assembly.GetExecutingAssembly());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseHttpMetrics();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapModuleEndpoints();

app.MapMetrics();

app.Run();