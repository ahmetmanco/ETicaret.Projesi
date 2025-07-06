using System.Text;
using System.Text.Json.Serialization;
using _01_Domain.Layer.Entities;
using _02_Application.Layer;
using _02_Application.Layer.Validations.Product;
using _03_Infrastructure.Layer;
using _03_Infrastructure.Layer.Filters;
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

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidation>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", opt =>
    {
        opt.TokenValidationParameters = new()
        {
            ValidateAudience = true, //hangi sitelerin
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]!))
        };
    });

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddDbContext<AppDbContext>(options =>
     options.UseNpgsql(connectionString));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DependencyResolver());
});

builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders(); 

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login"; 
    options.AccessDeniedPath = "/access-denied";
});

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();        

app.UseAuthentication();   
app.UseAuthorization();     

app.MapControllers();

app.Run();