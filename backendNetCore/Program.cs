using backendNetCore.MealPlans.Application.Internal.CommandServices;
using backendNetCore.MealPlans.Application.Internal.QueryServices;
using backendNetCore.MealPlans.Domain.Repositories;
using backendNetCore.MealPlans.Domain.Services;
using backendNetCore.MealPlans.Infrastructure.Repositories;
using backendNetCore.Recipes.Application.Internal.CommandServices;
using backendNetCore.Recipes.Application.Internal.QueryServices;
using backendNetCore.Recipes.Domain.Repositories;
using backendNetCore.Recipes.Domain.Services;
using backendNetCore.Recipes.Infrastructure.Persistence.EFC.Repositories;

using backendNetCore.Recommendations.Application.Internal.CommandServices;
using backendNetCore.Recommendations.Application.Internal.QueryServices;
using backendNetCore.Recommendations.Domain.Model.Repositories;
using backendNetCore.Recommendations.Infrastructure.Persistence.EFC.repositories;
using backendNetCore.Recommendations.Infrastructure.Persistence.EFC.Repositories;

using backendNetCore.Shared.Domain.Repositories;
using backendNetCore.Shared.Infrastructure.Interfaces.ASP.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Configuration;
using backendNetCore.Shared.Infrastructure.Persistence.Repositories;
using backendNetCore.Tracking.Application.Internal.CommandServices;
using backendNetCore.Tracking.Application.Internal.QueryServices;
using backendNetCore.Tracking.Domain.Repositories;
using backendNetCore.Tracking.Domain.Services;
using backendNetCore.Tracking.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the Container

// Configure Lower Case URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configure Kebab Case Route Naming Convention
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy =>
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());
});

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Verify Database Connection String
if (connectionString is null)
    throw new Exception("No connection string found");

// Configure Database Context and Logging Levels
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseMySQL(connectionString)
               .LogTo(Console.WriteLine, LogLevel.Information)
               .EnableSensitiveDataLogging()
               .EnableDetailedErrors();
    });
}
else if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseMySQL(connectionString)
               .LogTo(Console.WriteLine, LogLevel.Information)
               .EnableDetailedErrors();
    });
}

// Add Open API / Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AlimentatePlus API",
        Version = "v1",
        Description = "API for AlimentatePlus",
        Contact = new OpenApiContact
        {
            Name = "NutriSmart Dev Team",
            Email = "contact@nutrismart.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
    options.EnableAnnotations();
});

// ------------------ Dependency Injection ------------------

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Recipes Bounded Context
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IRecipeCommandService, RecipeCommandService>();
builder.Services.AddScoped<IRecipeQueryService, RecipeQueryService>();
builder.Services.AddScoped<IIngredientCommandService, IngredientCommandService>();
builder.Services.AddScoped<IIngredientQueryService, IngredientQueryService>();


// Recommendations Bounded Context
builder.Services.AddScoped<IRecommendationCommandService, RecommendationCommandService>();
builder.Services.AddScoped<IRecommendationQueryService, RecommendationQueryService>();
builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();
builder.Services.AddScoped<RecommendationTemplateRepository>(); // No tiene interfaz todavía

// Meal Plans Bounded Context Injection Configuration
builder.Services.AddScoped<IMealPlanRepository, MealPlanRepository>();
builder.Services.AddScoped<IMealPlanCommandService, MealPlanCommandService>();
builder.Services.AddScoped<IMealPlanQueryService, MealPlanQueryService>();

// Tracking Bounded Context
builder.Services.AddScoped<ITrackingRepository, TrackingRepository>();
builder.Services.AddScoped<ITrackingCommandService, TrackingCommandService>();
builder.Services.AddScoped<ITrackingQueryService, TrackingQueryService>();

builder.Services.AddScoped<IMealPlanEntryRepository, MealPlanEntryRepository>();
builder.Services.AddScoped<IMealPlanTypeRepository, MealPlanTypeRepository>();

builder.Services.AddScoped<ITrackingMacronutrientRepository, TrackingMacronutrientRepository>();
builder.Services.AddScoped<ITrackingMacronutrientCommandService, TrackingMacronutrientCommandService>();
builder.Services.AddScoped<ITrackingMacronutrientQueryService, TrackingMacronutrientQueryService>(); // <-- aquí
builder.Services.AddScoped<ITrackingGoalRepository, TrackingGoalRepository>();
builder.Services.AddScoped<ITrackingGoalCommandService, TrackingGoalCommandService>();
builder.Services.AddScoped<ITrackingGoalQueryService, TrackingGoalQueryService>();


var app = builder.Build();

// Ensure Database is created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowAllPolicy");

app.MapControllers();

app.Run();
