using Microsoft.EntityFrameworkCore;
using demoapi.Data; // EducationDbContext i�in do�ru namespace
using AutoMapper; // AutoMapper i�in ekleme
using System.Reflection;
using demoapi.MappingProfiles;
//using AutoMapper.Extensions.Microsoft.DependencyInjection; // Add this line

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// AutoMapper Entegrasyonu
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); // MappingProfiles i�in otomatik tarama

// DbContext Entegrasyonu
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS Politikas�
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Development ortam� i�in Swagger yap�land�rmas�
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
