using Microsoft.EntityFrameworkCore;
using demoapi.Data; // EducationDbContext için doðru namespace
using AutoMapper; // AutoMapper için ekleme
using System.Reflection;
using demoapi.MappingProfiles;
using demoapi.Repository.Interfaces;
using demoapi.Services.Interfaces;
using demoapi.Repository.Implementations;
using demoapi.Services.Implementations;
using demoapi.Repositories.Implementations;
using demoapi.Repositories.Interfaces;
using demoapi.Repository.Interfaces.demoapi.Repositories.Interfaces;

//using AutoMapper.Extensions.Microsoft.DependencyInjection; // Add this line

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IGradeRepository, GradeRepository>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<ITopicRepository, TopicRepository>();
builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();



// AutoMapper Entegrasyonu
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); // MappingProfiles için otomatik tarama
// DbContext Entegrasyonu
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS Politikasý
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

// Development ortamý için Swagger yapýlandýrmasý
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
