using Microsoft.EntityFrameworkCore;
using OxygenMeasurementApi;
using OxygenMeasurementApi.Data.Context;
using OxygenMeasurementApi.Services.OxygenMeasurementService;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("OxygenMeasurementPolicy", corsPolicyBuilder =>
    {
        // make specific for angular ?
        corsPolicyBuilder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IOxygenMeasurementService, OxygenMeasurementService>();
builder.Services.AddScoped<IOxygenDbContext, OxygenDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();


var conn = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<OxygenDbContext>(options => { options.UseNpgsql(conn); });


var app = builder.Build();


using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<OxygenDbContext>();
dbContext.Database.Migrate();

// ensure that the database is created and updated with the migrations.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<OxygenMeasurementHub>("oxygenMeasurementHub");

app.Run();

public partial class Program
{
}