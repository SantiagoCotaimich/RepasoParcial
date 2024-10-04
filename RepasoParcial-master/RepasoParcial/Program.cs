using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RepasoParcial.Models;
using RepasoParcial.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CineDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault")));
builder.Services.AddScoped<IPeliculaRepository, PeliculaRepository>();

//agregar las anteriores dos lineas como estan, pero adecuar a los nombres de un dbcontext y repositorios

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
