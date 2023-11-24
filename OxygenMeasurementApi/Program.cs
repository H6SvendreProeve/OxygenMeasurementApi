using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi;
using OxygenMeasurementApi.Data;
using OxygenMeasurementApi.Services.OxygenMeasurementService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*builder.Services.AddCors(options =>
{
    options.AddPolicy("OxygenMeasurementPolicy", corsPolicyBuilder =>
    {
        // port 4200 is angular web applikation
        corsPolicyBuilder.WithOrigins("http://localhost:4200", "https://localhost:4200", "https://192.168.1.2:4200",
                "http://192.168.1.2:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});*/

builder.Services.AddScoped<IOxygenMeasurementService, OxygenMeasurementService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conn = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<OxygenDbContext>(options => { options.UseNpgsql(conn); });


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OxygenDbContext>();
    // ensure that the database is created and updated with the migrations.
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();