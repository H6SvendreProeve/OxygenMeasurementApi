using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OxygenMeasurementApi;
using OxygenMeasurementApi.Authorization.Filters;
using OxygenMeasurementApi.Data.Context;
using OxygenMeasurementApi.Middlewares;
using OxygenMeasurementApi.Services.ApiKeyService;
using OxygenMeasurementApi.Services.OxygenMeasurementService;
using OxygenMeasurementApi.Services.OxygenMeasurementSystemService;
using OxygenMeasurementApi.Logger;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificNetwork",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.SetIsOriginAllowed(origin => origin.StartsWith("http://192.168.") || origin.StartsWith("http://10."))
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

// Dependency injection setup for services and filters.
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddScoped<ApiKeyAuthorizationFilter>();
builder.Services.AddScoped<IOxygenMeasurementService, OxygenMeasurementService>();
builder.Services.AddScoped<IOxygenMeasurementSystemService, OxygenMeasurementSystemService>();
builder.Services.AddScoped<IOxygenDbContext, OxygenDbContext>();
builder.Services.AddSingleton<Logger>();
builder.Services.AddSingleton<SmtpConfigLoader>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>

{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OxygenMeasurementApi", Version = "v1" });

    // Define security scheme for X-Api-Key
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API key needed to access the endpoints",
        In = ParameterLocation.Header,
        Name = "X-Api-Key",
        Type = SecuritySchemeType.ApiKey
    });

    // Define security scheme for X-System-Id
    c.AddSecurityDefinition("SystemId", new OpenApiSecurityScheme
    {
        Description = "System ID needed to access the endpoints",
        In = ParameterLocation.Header,
        Name = "X-System-Id"
    });

    // Apply security to endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] { }
        },
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "SystemId"
                }
            },
            new string[] { }
        }
    });
});


var conn = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure DbContext for PostgreSQL using the provided connection string.
builder.Services.AddDbContext<OxygenDbContext>(options => { options.UseNpgsql(conn); });


var app = builder.Build();


// ensure that the database is created and updated with the migrations.
try
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<OxygenDbContext>();
    dbContext.Database.Migrate();
}
catch (Exception e)
{
    await Logger.LogAsync("error during database migrations : exception \n " + e);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

// Enable authorization and map controllers.
app.UseAuthorization();

app.MapControllers();

// Use custom middleware for handling global exceptions.
app.UseMiddleware<GlobalExceptionMiddleware>();

app.Run();

public partial class Program
{
}