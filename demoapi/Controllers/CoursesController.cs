using AutoMapper;
using demoapi.Data;
using demoapi.DTO;
using demoapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CoursesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/courses
    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        // Courses ile Topics iliþkisini de dahil ederek getiriyoruz
        var courses = await _context.Courses
            .Include(c => c.Topics)
            .ToListAsync();

        // AutoMapper kullanarak DTO'ya dönüþtürme
        var courseDtos = _mapper.Map<List<CourseDto>>(courses);

        return Ok(courseDtos);
    }

    // POST: api/courses
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CourseDto courseDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Model doðrulama hatalarýný döner
        }

        // DTO'dan Model'e dönüþüm
        var course = _mapper.Map<Course>(courseDto);

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        // Kaydedilen Course'yi tekrar DTO'ya dönüþtürerek döndürüyoruz
        var createdCourseDto = _mapper.Map<CourseDto>(course);

        return CreatedAtAction(nameof(GetCourses), new { id = course.CourseId }, createdCourseDto);
    }
}
