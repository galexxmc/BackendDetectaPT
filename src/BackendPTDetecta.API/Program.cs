using BackendPTDetecta.Application;
using BackendPTDetecta.Infrastructure.Persistence;
using BackendPTDetecta.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BackendPTDetecta.Application.Common.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. CAPA INFRASTRUCTURE: Configuración de Base de Datos
// Obtenemos la cadena de conexión del appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
    
// Esto le dice a la API: "Cuando un Handler pida IApplicationDbContext, dale la instancia de ApplicationDbContext"
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

// 2. CAPA INFRASTRUCTURE: Configuración de Identity (Auth)
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints(); // Nuevo en .NET 8/9 para endpoints automáticos

builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);
    
builder.Services.AddAuthorization();

// 3. CAPA API: Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Esto carga toda la lógica de negocio (MediatR, Validadores)
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configuración del Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Importante: El orden importa
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();