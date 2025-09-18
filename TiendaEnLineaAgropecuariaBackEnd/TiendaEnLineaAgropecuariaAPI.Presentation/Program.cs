using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.UseCases.AuthUseCases.AuthCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.BodegasUseCases.BodegasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.BodegasUseCases.BodegasQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.CarritosUseCases.CarritosQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.InventariosUseCases.InventariosCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.InventariosUseCases.InventariosQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.TiposProductoUseCases.TiposProductoCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.TiposProductoUseCases.TiposProductoQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.UsuariosUseCases.UsuariosCommands;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioAuth;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioBodegas;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioCarrito;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioCategorias;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioInventario;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioTipoProductos;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioUsuarios;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGen(opciones =>
{
    opciones.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    opciones.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

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
builder.Services.AddScoped<IRepositorioCategorias, RepositorioCategorias>();
builder.Services.AddScoped<IRepositorioTipoProductos, RepositorioTipoProductos>();
builder.Services.AddScoped<IRepositorioBodegas, RepositorioBodegas>();
builder.Services.AddScoped<IRepositorioInventario, RepositorioInventario>();
builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosAzure>();
builder.Services.AddTransient<IRepositorioCarrito, RepositorioCarrito>();

// Casos de uso
// Casos de uso Login y Registro
builder.Services.AddTransient<RegistrarUsuarioConEmail>();
builder.Services.AddTransient<LoginConEmail>();
builder.Services.AddTransient<LoginConGoogle>();
// Casos de uso Categorias
builder.Services.AddTransient<GetAllCategorias>();
builder.Services.AddTransient<GetCategoriaById>();
builder.Services.AddTransient<PostCategoria>();
builder.Services.AddTransient<PutCategorias>();
builder.Services.AddTransient<DeleteCategorias>();
// Casos de uso TipoProducto
builder.Services.AddTransient<GetAllTiposProducto>();
builder.Services.AddTransient<GetTiposProductoById>();
builder.Services.AddTransient<PostTipoProducto>();
builder.Services.AddTransient<PutTipoProducto>();
builder.Services.AddTransient<DeleteTipoProducto>();
// Casos de uso Bodegas
builder.Services.AddTransient<GetAllBodegas>();
builder.Services.AddTransient<GetBodegaById>();
builder.Services.AddTransient<PostBodegas>();
builder.Services.AddTransient<PutBodegas>();
builder.Services.AddTransient<DeleteBodegas>();
// Casos de uso Inventarios
builder.Services.AddTransient<GetAllInventarios>();
builder.Services.AddTransient<GetInventarioById>();
builder.Services.AddTransient<PostInventario>();
builder.Services.AddTransient<PutInventario>();
builder.Services.AddTransient<DeleteInventario>();
// Casos de uso Carritos
builder.Services.AddTransient<GetCarritoByUserId>();

// Servicios extras
builder.Services.AddTransient<CrearToken>();
builder.Services.AddTransient<ValidarToken>();
builder.Services.AddTransient<GenerarPayloadDeGoogle>();
builder.Services.AddTransient<EnviarCorreos>();
builder.Services.AddTransient<BuscarAppUsuario>();
builder.Services.AddTransient<ResetearPassword>();
builder.Services.AddTransient<ServicioUsuarios>();


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
