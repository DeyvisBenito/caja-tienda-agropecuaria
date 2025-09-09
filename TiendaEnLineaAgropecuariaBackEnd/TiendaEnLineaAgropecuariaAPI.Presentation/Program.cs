using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.UseCases.AuthUseCases.AuthCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.UsuariosUseCases.UsuariosCommands;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioAuth;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioUsuarios;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGen();

// Configurando CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(opcionesCORS =>
    {
        opcionesCORS.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); // TODO: Configurar CORS al origen especifico
    });
});

builder.Services.AddControllers().AddJsonOptions(option =>
            option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
                .AddNewtonsoftJson();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

// Configurando EntityFrameworkCore
builder.Services.AddDbContext<ApplicationDBContext>(opciones => 
         opciones.UseSqlServer("name=DefaultConnection", sqlOpciones => 
         sqlOpciones.MigrationsAssembly("TiendaEnLineaAgropecuaria.Infraestructure")));

// Configurando Identity
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();

// Acceder al contexto Http desde cualquier clase
builder.Services.AddHttpContextAccessor();

// Configurando authentication de usuarios con JWT
builder.Services.AddAuthentication().AddJwtBearer(opciones =>
{
    opciones.MapInboundClaims = false;

    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["llavejwt"]!)),
        ClockSkew = TimeSpan.Zero
    };
});



//              Configurando Servicios
// Repositorios
builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();
builder.Services.AddScoped<IRepositorioAuth, RepositorioAuth>();

// Casos de uso
builder.Services.AddTransient<RegistrarUsuarioConEmail>();
builder.Services.AddTransient<LoginConEmail>();
builder.Services.AddTransient<LoginConGoogle>();

// Servicios extras
builder.Services.AddTransient<CrearToken>();
builder.Services.AddTransient<ValidarToken>();
builder.Services.AddTransient<GenerarPayloadDeGoogle>();
builder.Services.AddTransient<EnviarCorreos>();
builder.Services.AddTransient<BuscarAppUsuario>();
builder.Services.AddTransient<ResetearPassword>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
