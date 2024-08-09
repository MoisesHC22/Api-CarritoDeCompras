using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.RemoteServices;
using TiendaServicios.Api.CarritoDeCompra.Aplicaction;
using TiendaServicios.Api.CarritoDeCompra.Persistencia;
using TiendaServicios.Api.CarritoDeCompra.RemoteInterface;
using TiendaServicios.Api.CarritoDeCompra.RemoteServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
builder.Services.AddScoped<ILibroService, LibrosService>();
builder.Services.AddScoped<IAutorService, AutoresService>();
builder.Services.AddScoped<ICuponService, CuponesService>();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<CarritoContexto>(opciones =>
opciones.UseSqlServer("name=DefaulConnection"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    });
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddHttpClient("Libros", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:Libros"]);
});

builder.Services.AddHttpClient("Autores", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:Autores"]);
});

builder.Services.AddHttpClient("Cupones", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:Cupones"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
