using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using tpAPI_ASINCRO.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddMvc().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

//string connectionString = builder.Configuration.GetSection("ConnectionStrings").GetSection("Default").Value;
string connectionString = builder.Configuration.GetConnectionString("Default");
//string connectionString = builder.Configuration.GetSection("ParametroImportante").Value;
//int parametro = int.Parse(builder.Configuration.GetSection("ParametroImportante").Value);

builder.Services.AddDbContext<IDBM_5Context>(config =>
{
    config.UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
//app.UseAuthentication(); //quien sos
app.UseAuthorization(); //ya se quien sos y queiro saber que podes hacer
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
//app.RunAsync();
