using backendNetCore.MealPlans.Application.Internal.CommandServices;
using backendNetCore.MealPlans.Application.Internal.QueryServices;
using backendNetCore.MealPlans.Domain.Repositories;
using backendNetCore.MealPlans.Domain.Services;
using backendNetCore.MealPlans.Infrastructure.Repositories;
using backendNetCore.Shared.Domain.Repositories;
using backendNetCore.Shared.Infrastructure.Interfaces.ASP.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddRouting(options => options.LowercaseUrls = true);


// Configure Kebab Case Route Naming Convention
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add Database Connection  
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Verify Database Connection String
if (connectionString is null)
    throw new Exception("Database Connection String is null");

// Configure Database Context and Logging levels
if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    });
else if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors();
    });

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Meal Plans Bounded Context Injection Configuration
builder.Services.AddScoped<IMealPlanRepository, MealPlanRepository>();
builder.Services.AddScoped<IMealPlanCommandService, MealPlanCommandService>();
builder.Services.AddScoped<IMealPlanQueryService, MealPlanQueryService>();

var app = builder.Build();

using( var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
