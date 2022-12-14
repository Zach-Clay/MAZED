// app starting point

using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using MazedDB.Data;
using Newtonsoft.Json;
using api_backend.Procedures;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc().AddControllersAsServices();

//add db context service so our context can create our controller
builder.Services.AddDbContext<MazedDBContext>(
options =>
{
    //tell to use our string and versuon
    options.UseMySql(builder.Configuration.GetConnectionString("DB"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
});

//adding the services to ignore referenceloop so we can fix error thrown for object disconnected
builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

//service for our procedure just as we did for our original context
//builder.Services.AddDbContext<MazedDBContextProcedures>(
//options =>
//{
//    options.UseMySql(builder.Configuration.GetConnectionString("DB"),
//    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

