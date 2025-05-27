using System.Text.Json.Serialization;
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
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuration (ConnectionStrings & Azure Storage)
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
var azureBlobConnection = builder.Configuration.GetSection("Storage")["Azure"];

// 2. Services: MVC + JSON Options
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

// 3. FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidation>();

// 4. CORS
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// 5. PostgreSQL DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// 6. Application Layers
builder.Services.AddInfrastructureservices();
builder.Services.AddApplicationServices();

// 7. Blob Storage Client (Azure)
builder.Services.AddSingleton(_ => new BlobServiceClient(azureBlobConnection));
builder.Services.AddStorage<AzureStorage>();

// 8. MediatR (CQRS)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(_02_Application.Layer.ServiceRegistration).Assembly);
});

// 9. Autofac Dependency Injection
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DependencyResolver());
});

// 10. Swagger (Opsiyonel)
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// 11. Middleware pipeline
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();        // wwwroot kullanıyorsan
app.UseRouting();
app.UseCors();               // CORS ayarları
app.UseAuthorization();
app.MapControllers();

app.Run();
