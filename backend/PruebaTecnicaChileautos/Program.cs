using PruebaTecnicaChileautos.Core.Interfaces;
using PruebaTecnicaChileautos.Infrastructure.Clients;
using PruebaTecnicaChileautos.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RickAndMortyOptions>(
    builder.Configuration.GetSection("RickAndMortyApi"));

builder.Services.AddHttpClient<IRickAndMortyApiClient, RickAndMortyApiClient>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllDev", policy =>
    {
        policy.AllowAnyOrigin()       
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAllDev");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
