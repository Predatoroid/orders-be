using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Npgsql;
using Orders.Abstractions;
using Orders.Extensions;
using Orders.Presentation;
using Orders.Repositories;
using Prometheus;
using Serilog;
using Serilog.Sinks.Graylog;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .WriteTo.Console()
    .WriteTo.Graylog(new GraylogSinkOptions
    {
        HostnameOrAddress = "graylog",
        Port = 12201,
        Facility = "MinimalAPI",
        MinimumLogEventLevel = Serilog.Events.LogEventLevel.Information
    })
    .CreateLogger();

builder.Host.UseSerilog();

// builder.Services.AddScoped<IDbConnection>(sp =>
//     new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<IDbConnection>(sp => new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<ICustomerFieldRepository, CustomerFieldRepository>();

builder.Services.AddServicesByReflection(Assembly.GetExecutingAssembly());

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

app.AddOrdersEndpoints();
app.AddCustomerEndpoints();
app.AddCustomerFieldsEndpoints();

app.MapMetrics();

app.Run();