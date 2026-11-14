
using Fotografia.Domain.Entities;
using Fotografia.Domain.Interfaces;
using Fotografia.Domain.Repository;
using Fotografia.Infrastructure.Context;
using Fotografia.Infrastructure.Contract;
using Fotografia.Infrastructure.Repositories;
using Fotografia.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FotografiaContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("FotografiaConnection")
    );
});


builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddScoped<IFotografoRepository, FotografoRepository>();

builder.Services.AddScoped<ISesionRepository, SesionRepository>();

builder.Services.AddScoped<IRepository<Paquete>, PaqueteRepository>();




builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<ISesionService, SesionService>();
builder.Services.AddScoped<IEntregaService, EntregaService>();
builder.Services.AddScoped<IReporteService, ReporteService>();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
