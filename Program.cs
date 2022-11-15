using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ContosoPizza.Services;
using ContosoPizza.Models;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<PizzaDb>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo {
         Title = "PizzaStore API",
         Description = "Making the Pizzas you love",
         Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
    });
}
app.MapGet("/", () => "Hello World!");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync());
app.Run();
