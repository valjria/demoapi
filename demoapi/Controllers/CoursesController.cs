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
        // Courses ile Topics ili�kisini de dahil ederek getiriyoruz
        var courses = await _context.Courses
            .Include(c => c.Topics)
            .ToListAsync();

        // AutoMapper kullanarak DTO'ya d�n��t�rme
        var courseDtos = _mapper.Map<List<CourseDto>>(courses);

        return Ok(courseDtos);
    }

    // POST: api/courses
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CourseDto courseDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Model do�rulama hatalar�n� d�ner
        }

        // DTO'dan Model'e d�n���m
        var course = _mapper.Map<Course>(courseDto);

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        // Kaydedilen Course'yi tekrar DTO'ya d�n��t�rerek d�nd�r�yoruz
        var createdCourseDto = _mapper.Map<CourseDto>(course);

        return CreatedAtAction(nameof(GetCourses), new { id = course.CourseId }, createdCourseDto);
    }
}
