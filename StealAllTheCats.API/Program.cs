using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using StealAllTheCats.API.Swagger;
using StealAllTheCats.Application.Mappings;
using StealAllTheCats.Application.Services;
using StealAllTheCats.Core.Interfaces;
using StealAllTheCats.Infrastructure.ApiClients;
using StealAllTheCats.Infrastructure.Database;
using StealAllTheCats.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CatApiOptions>(builder.Configuration.GetSection("CatApiOptions"));
builder.Services.AddHttpClient<ICatApiClient, CatApiClient>();
builder.Services.AddScoped<ICatService, CatService>();

// Clear the default logging providers to use Serilog
builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, config) =>
{
    config
    .ReadFrom.Configuration(context.Configuration)
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message}{NewLine}{Exception}")
    .Enrich.FromLogContext();
});

// Add controllers and configure JSON options
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Add API Explorer and Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "StealAllTheCats API",
        Version = "v1",
        Description = "API for fetching and storing cat images",
        Contact = new OpenApiContact
        {
            Name = "Vasileios Argyroglou",
            Email = "b.argyroglou@gmail.com"
        }
    });
    options.DescribeAllParametersInCamelCase();
    options.OperationFilter<SwaggerJsonRequestBodyOperationFilter>();
});

// Register Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
