// app starting point

using Microsoft.EntityFrameworkCore;
using api_backend.Data;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//use builder to add context -- in mem DB
//builder.Services.AddDbContext<UsersAPIDBContext>(options => options.UseInMemoryDatabase("TEAM2_DB"));
//builder.Services.AddDbContext<UsersAPIDBContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DB")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

