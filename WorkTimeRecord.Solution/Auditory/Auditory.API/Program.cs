using Auditory.API.Data.Context;
using Carter;
using Components.Communication.MessageBroker;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Auditory Api",
        Description = "API de Auditoría de registros de horario de los empleados",
        Version = "v1"
    });
});

builder.Services.AddCarter();
builder.Services.AddMediatR(configuration => { configuration.RegisterServicesFromAssembly(typeof(Program).Assembly); });
builder.Services.AddScoped(typeof(IAuditoryContext), typeof(AuditoryMongoContext));

builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");//CORS

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auditory V1"));

app.MapCarter();

app.Run();
