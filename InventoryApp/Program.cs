var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Inventory Management API",
        Version = "v1",
        Description = "A clean .NET 10 REST API for managing an inventory"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory Management API v1");
    c.RoutePrefix = string.Empty; // Swagger at root URL "/"
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
