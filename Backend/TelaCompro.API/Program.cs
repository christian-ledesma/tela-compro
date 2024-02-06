using Microsoft.EntityFrameworkCore;
using TelaCompro.Infrastructure.Extensions;
using TelaCompro.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepositories();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    StoreContext context = scope.ServiceProvider.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();
    var seedIsActive = builder.Configuration.GetValue<bool>("EntityFramework:SeedData");
    if (seedIsActive)
    {
        InfrastructureServiceCollection.Seed(context);
    }
}

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
