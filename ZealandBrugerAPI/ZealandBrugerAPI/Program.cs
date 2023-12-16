using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZealandBrugerAPI.EDbContext;

var builder = WebApplication.CreateBuilder(args);


// Configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BrugerDbContext>();





// Configure DbContet
var connectionString = configuration.GetConnectionString("DefaultConnectionSQLAuth");
builder.Services.AddDbContext<BrugerDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add CORS

var allowAll = "AllowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAll, builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();
app.UseCors(allowAll);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
