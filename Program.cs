using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System;
using QRCulturalBackEnd.AppDbContext;

var builder = WebApplication.CreateBuilder(args);


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://gabrielclisboa.github.io")
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                      });
});


var app = builder.Build();


app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

// Teste de conexão com o banco de dados
TestDatabaseConnection(app.Services);

app.Run();

// Método para testar a conexão com o banco de dados
void TestDatabaseConnection(IServiceProvider services)
{
    using (var scope = services.CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;

        try
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.OpenConnection();
            context.Database.CloseConnection();
            Console.WriteLine("Conexão com o banco de dados foi bem-sucedida.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Falha ao conectar com o banco de dados: " + ex.Message);
        }
    }
}
