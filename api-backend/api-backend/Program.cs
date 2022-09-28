// app starting point

using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Newtonsoft.Json;
using api_backend.Procedures;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adding the services to ignore referenceloop so we can fix error thrown for object disconnected
builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);


//service for our procedure just as we did for our original context
//builder.Services.AddDbContext<MazedDBContextProcedures>(
//options =>
//{
//    options.UseMySql(builder.Configuration.GetConnectionString("DB"),
//    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
//});

//Services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app cors
app.UseCors("corsapp");
//app.UseCors(prodCorsPolicy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

