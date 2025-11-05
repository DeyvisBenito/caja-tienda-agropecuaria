using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.UseCases.AuthUseCases.AuthCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.CarritoDetUseCases.CarritoDetCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.CarritoDetUseCases.CarritoDetQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.CarritosUseCases.CarritosQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.CategoriasUseCases.CategoriasQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.ClientesUseCases.ClientesCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.ClientesUseCases.ClientesQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.ComprasUseCases.ComprasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.ComprasUseCases.ComprasQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.ConversionesUseCases.ConversionesQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.DetCompraUseCases.DetCompraCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.DetCompraUseCases.DetCompraQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.DetVentaUseCases.DetVentaCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.DetVentaUseCases.DetVentaQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.InventariosUseCases.InventariosCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.InventariosUseCases.InventariosQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.ProveedoresUseCases.ProveedoresCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.ProveedoresUseCases.ProveedoresQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.ReportesUseCases.ReportesQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.SucursalesUseCases.SucursalesCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.SucursalesUseCases.SucursalesQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.TiposMedidaUseCases.TiposMedidaQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.TiposProductoUseCases.TiposProductoCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.TiposProductoUseCases.TiposProductoQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.UnidadMedidaUseCases.UnidadMedidaQuerys;
using TiendaEnLineaAgropecuaria.Application.UseCases.UsuariosUseCases.UsuariosCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.VentasUseCases.VentasCommands;
using TiendaEnLineaAgropecuaria.Application.UseCases.VentasUseCases.VentasQuerys;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioAuth;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioBodegas;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioCarrito;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioCarritoDet;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioCategorias;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioClientes;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioCompras;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioConversiones;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioDetCompra;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioDetVenta;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioInventario;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioProveedores;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioReportes;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioTipoMedida;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioTipoProductos;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioUnidadMedida;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioUsuarios;
using TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioVentas;
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

// Policy para solo admin
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireClaim("rol", "admin"));
});



//              Configurando Servicios
// Repositorios
builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();
builder.Services.AddScoped<RepositorioUsuarios>();
builder.Services.AddScoped<IRepositorioAuth, RepositorioAuth>();
builder.Services.AddScoped<IRepositorioCategorias, RepositorioCategorias>();
builder.Services.AddScoped<IRepositorioTipoProductos, RepositorioTipoProductos>();
builder.Services.AddScoped<IRepositorioSucursal, RepositorioSucursal>();
builder.Services.AddScoped<IRepositorioInventario, RepositorioInventario>();
builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosAzure>();
builder.Services.AddTransient<IRepositorioCarrito, RepositorioCarrito>();
//builder.Services.AddTransient<IRepositorioCarritoDet, RepositorioCarritoDet>();
builder.Services.AddTransient<IRepositorioCompras, RepositorioCompras>();
builder.Services.AddTransient<IRepositorioTipoMedida, RepositorioTipoMedida>();
builder.Services.AddTransient<IRepositorioProveedores, RepostiroioProveedores>();
builder.Services.AddTransient<IRepositorioUnidadMedida, RepositorioUnidadMedida>();
builder.Services.AddTransient<IRepositorioDetCompra, RepositorioDetCompra>();
builder.Services.AddTransient<IRepositorioClientes, RepositorioClientes>();
builder.Services.AddTransient<IRepositorioVentas, RepositorioVentas>();
builder.Services.AddTransient<IRepositorioDetVenta, RepositorioDetVenta>();
builder.Services.AddTransient<IRepositorioConversiones, RepositorioConversiones>();
builder.Services.AddTransient<IRepositorioReportes, RepositorioReportes>();

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
// Casos de uso Sucursales
builder.Services.AddTransient<GetAllSucursales>();
builder.Services.AddTransient<GetSucursalById>();
builder.Services.AddTransient<PostSucursales>();
builder.Services.AddTransient<PutSucursales>();
builder.Services.AddTransient<DeleteSucursales>();
// Casos de uso Inventarios
builder.Services.AddTransient<GetAllInventarios>();
builder.Services.AddTransient<GetInventarioById>();
builder.Services.AddTransient<GetInventarioByCodigo>();
builder.Services.AddTransient<PostInventario>();
builder.Services.AddTransient<PutInventario>();
builder.Services.AddTransient<DeleteInventario>();
// Casos de uso Carritos
builder.Services.AddTransient<GetCarritoByUserId>();
// Casos de uso Carrito Detalles
//builder.Services.AddTransient<GetAllCarritoDet>();
//builder.Services.AddTransient<GetCarritoDetById>();
//builder.Services.AddTransient<PostCarritoDet>();
//builder.Services.AddTransient<PutCarritoDet>();
//builder.Services.AddTransient<DeleteCarritoDet>();
// Casos de uso Compras
builder.Services.AddTransient<GetAllCompras>();
builder.Services.AddTransient<GetCompraById>();
builder.Services.AddTransient<GetCompraPendiente>();
builder.Services.AddTransient<PostCompras>();
builder.Services.AddTransient<PutCompras>();
builder.Services.AddTransient<DeleteCompras>();
builder.Services.AddTransient<CancelCompra>();
builder.Services.AddTransient<ProcesarCompra>();
// Casos de uso Tipos Medida
builder.Services.AddTransient<GetAllTiposMedida>();
// Casos de uso Unidades de Medida
builder.Services.AddTransient<GetAllUnidadMedida>();
// Casos de uso Proveedores
builder.Services.AddTransient<GetAllProveedores>();
builder.Services.AddTransient<GetProveedorById>();
builder.Services.AddTransient <PostProveedor>();
builder.Services.AddTransient<PutProveedor>();
builder.Services.AddTransient<DeleteProveedor>();
// Casos de uso de Detalles de compra
builder.Services.AddTransient<GetAllDetCompra>();
builder.Services.AddTransient<GetAllDetCompraByCompraId>();
builder.Services.AddTransient<GetDetCompraById>();
builder.Services.AddTransient<GetDetByInvId>();
builder.Services.AddTransient<PostDetCompra>();
builder.Services.AddTransient<PutDetCompra>();
builder.Services.AddTransient<DeleteDetCompra>();
// Casos de uso de Clientes
builder.Services.AddTransient<GetAllClientes>();
builder.Services.AddTransient<GetClienteById>();
builder.Services.AddTransient<GetClienteByVentaId>();
builder.Services.AddTransient<PostCliente>();
builder.Services.AddTransient<PutCliente>();
builder.Services.AddTransient<DeleteCliente>();
// Casos de uso Ventas
builder.Services.AddTransient<GetAllVentas>();
builder.Services.AddTransient<GetVentaById>();
builder.Services.AddTransient<PostVenta>();
builder.Services.AddTransient<PutVenta>();
builder.Services.AddTransient<DeleteVenta>();
builder.Services.AddTransient<CancelVenta>();
builder.Services.AddTransient<ProcesarVenta>();
// Casos de uso de Detalles de Venta
builder.Services.AddTransient<GetAllDetVentas>();
builder.Services.AddTransient<GetAllDetVentaByVentaId>();
builder.Services.AddTransient<GetDetVentaById>();
builder.Services.AddTransient<GetDetVentaByInvId>();
builder.Services.AddTransient<GetDetVentaByInvIdUpd>();
builder.Services.AddTransient<PostDetVenta>();
builder.Services.AddTransient<PutDetVenta>();
builder.Services.AddTransient<DeleteDetVenta>();
// Casos de uso de conversiones
builder.Services.AddTransient<GetDescuentoByConversion>();
// Casos de uso de reportes
builder.Services.AddTransient<GetBestClientesPorVentas>();
builder.Services.AddTransient<GetVentasDelDia>();
builder.Services.AddTransient<GetComprasDelDia>();
builder.Services.AddTransient<GetBestProveedores>();

// Servicios extras
builder.Services.AddTransient<CrearToken>();
builder.Services.AddTransient<ValidarToken>();
builder.Services.AddTransient<GenerarPayloadDeGoogle>();
builder.Services.AddTransient<EnviarCorreos>();
builder.Services.AddTransient<BuscarAppUsuario>();
builder.Services.AddTransient<ResetearPassword>();
builder.Services.AddTransient<ServicioUsuarios>();
builder.Services.AddTransient<ServicioInventarios>();


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
