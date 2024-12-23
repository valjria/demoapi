using demoapi.Data;
using demoapi.DTO;
using demoapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly EducationDbContext _context;

    public CoursesController(EducationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        // Courses ile Topics iliþkisini de dahil ederek getiriyoruz
        var courses = await _context.Courses
            .Include(c => c.Topics)
            .ToListAsync();

        return Ok(courses);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CourseDto courseDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Model doðrulama hatalarýný döner
            }

            // DTO'dan Model'e dönüþtürme
            var course = new Course
            {
                CourseName = courseDto.CourseName,
                Description = courseDto.Description,
                Topics = courseDto.Topics?.Select(t => new Topic
                {
                    TopicName = t.TopicName
                }).ToList()
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourses), new { id = course.CourseId }, course);
        }
        catch (Exception ex)
        {
            // Hatalarý logla
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}
