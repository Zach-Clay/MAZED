// app starting point

using Microsoft.EntityFrameworkCore;
using MySqlConnector;
<<<<<<< HEAD
=======
using MazedDB.Data;
>>>>>>> Evan/Dev
using Newtonsoft.Json;
using api_backend.Procedures;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

<<<<<<< HEAD
=======
//add db context service so our context can create our controller
//builder.Services.AddDbContext<MazedDBContext>(
/*options =>
{
    //tell to use our string and versuon
    options.UseMySql(builder.Configuration.GetConnectionString("DB"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
});*/

>>>>>>> Evan/Dev
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

<<<<<<< HEAD
//Services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

=======
>>>>>>> Evan/Dev
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

<<<<<<< HEAD
//app cors
app.UseCors("corsapp");
//app.UseCors(prodCorsPolicy);
=======
app.UseCors();
>>>>>>> Evan/Dev

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

