using BackendPTDetecta.Application;
using BackendPTDetecta.Infrastructure.Persistence;
using BackendPTDetecta.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BackendPTDetecta.Application.Common.Interfaces;
using BackendPTDetecta.Infrastructure;
using BackendPTDetecta.API.Services;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURACIÓN DE JWT (EL PORTERO) ---
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Secret"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero // Para que no espere 5 min extra si expira
    };
});

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
// Agregar capa de Application (El que ya tenías)
builder.Services.AddApplicationServices();

// Agregar capa de Infrastructure (EL NUEVO QUE ACABAMOS DE CREAR)
builder.Services.AddInfrastructureServices(builder.Configuration);

// 3. CAPA API: Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Configuración de Swagger con soporte para JWT
builder.Services.AddSwaggerGen(c =>
{
    // 1. Definimos el esquema de seguridad (Bearer)
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Escribe 'Bearer' [espacio] y luego tu token.\r\n\r\nEjemplo: \"Bearer eyJhbGciOiJIUzI1Ni...\""
    });

    // 2. Aplicamos la seguridad a todos los endpoints
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
// Esto carga toda la lógica de negocio (MediatR, Validadores)
builder.Services.AddApplicationServices();

// 1. Necesario para acceder al HttpContext (User, Claims, etc.)
builder.Services.AddHttpContextAccessor();

// 2. Conectar la interfaz con la implementación que acabamos de crear
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

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