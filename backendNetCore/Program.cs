using backendNetCore.IAM.Application.ACL.Services;
using backendNetCore.IAM.Application.Internal.CommandServices;
using backendNetCore.IAM.Application.Internal.OutboundServices;
using backendNetCore.IAM.Application.Internal.QueryServices;
using backendNetCore.IAM.Domain.Repositories;
using backendNetCore.IAM.Domain.Services;
using backendNetCore.IAM.Infrastructure.Hashing.BCrypt.Services;
using backendNetCore.IAM.Infrastructure.Persistence.EFC.Repositories;
using backendNetCore.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using backendNetCore.IAM.Infrastructure.Tokens.JWT.Configuration;
using backendNetCore.IAM.Infrastructure.Tokens.JWT.Services;
using backendNetCore.IAM.Interfaces.ACL;
using backendNetCore.MealPlans.Application.Internal.CommandServices;
using backendNetCore.MealPlans.Application.Internal.QueryServices;
using backendNetCore.MealPlans.Domain.Repositories;
using backendNetCore.MealPlans.Domain.Services;
using backendNetCore.MealPlans.Infrastructure.Repositories;
using backendNetCore.Profiles.Application.ACL;
using backendNetCore.Profiles.Application.Internal.CommandServices;
using backendNetCore.Profiles.Application.Internal.QueryServices;
using backendNetCore.Profiles.Domain.Repositories;
using backendNetCore.Profiles.Domain.Services;
using backendNetCore.Profiles.Infrastructure.Persistence.EFC.Repositories;
using backendNetCore.Profiles.Interfaces.ACL;
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
using backendNetCore.Tracking.Application.Internal.OutBoundServices.ACL;
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
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
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

builder.Services.AddScoped<IExternalProfileService, ExternalProfileService>();
builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();


// Profiles Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();

// IAM Bounded Context Injection Configuration

// TokenSettings Configuration

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

var app = builder.Build();

// Ensure Database is created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");

// Add Authorization Middleware to Pipeline
app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
