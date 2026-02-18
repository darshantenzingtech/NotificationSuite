using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using NotificationSuite;
using NotificationSuite.AzureServices.Interface;
using NotificationSuite.AzureServices.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")
    ));

builder.Services.AddScoped<IAzureNotificationService, AzureNotificationService>();
builder.Services.AddScoped<IEventNotificationService, EventNotificationService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "NotificationSuite API V1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
