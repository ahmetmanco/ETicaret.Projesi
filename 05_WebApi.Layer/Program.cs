using System.Text;
using System.Text.Json.Serialization;
using _01_Domain.Layer.Entities;
using _02_Application.Layer;
using _02_Application.Layer.Validations.Product;
using _03_Infrastructure.Layer;
using _03_Infrastructure.Layer.Filters;
using _03_Infrastructure.Layer.SeedData;
using _03_Infrastructure.Layer.Services.Storage;
using _04_Persistence.Layer.Context;
using _04_Persistence.Layer.IoC;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Azure.Storage.Blobs;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Configuration (ConnectionStrings & Azure Storage)
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
var azureBlobConnection = builder.Configuration.GetSection("Storage")["Azure"];

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilters>();
})
.ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true)
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.IncludeFields = true;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
});

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidation>();

// Authentication & Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", opt =>
    {
        opt.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]!)),
            RoleClaimType = "role" // JWT'deki rol claim'i
        };
    });

// Policy-Based Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin")
              .AddAuthenticationSchemes("Admin"));

    options.AddPolicy("CustomerPolicy", policy =>
        policy.RequireRole("Customer")
              .AddAuthenticationSchemes("Admin"));

    options.AddPolicy("SellerPolicy", policy =>
        policy.RequireRole("Seller")
              .AddAuthenticationSchemes("Admin"));

    // Örnek: Claim tabanlı policy
    options.AddPolicy("PremiumSeller", policy =>
        policy.RequireRole("Seller")
              .RequireClaim("Subscription", "Premium")
              .AddAuthenticationSchemes("Admin"));
});

// CORS
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
     options.UseNpgsql(connectionString));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Infrastructure & Application Services
builder.Services.AddInfrastructureservices();
builder.Services.AddApplicationServices();

// Blob Storage Client (Azure)
builder.Services.AddSingleton(_ => new BlobServiceClient(azureBlobConnection));
builder.Services.AddStorage<AzureStorage>();

// MediatR (CQRS)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(_02_Application.Layer.ServiceRegistration).Assembly);
});

// Autofac DI
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DependencyResolver());
});

// Identity Configuration
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/access-denied";
});

var app = builder.Build();



if (app.Environment.IsDevelopment() )
{
    await SeedData.Seed(app.Services);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();