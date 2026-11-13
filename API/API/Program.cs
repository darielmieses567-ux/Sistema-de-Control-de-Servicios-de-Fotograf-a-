
using Fotografia.Domain.Entities;
using Fotografia.Domain.Interfaces;
using Fotografia.Domain.Repository;
using Fotografia.Infrastructure.Context;
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


builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<PagoService>();
builder.Services.AddScoped<SesionService>();
builder.Services.AddScoped<EntregaService>();
builder.Services.AddScoped<ReporteService>();

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
