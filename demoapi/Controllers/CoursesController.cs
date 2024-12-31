using demoapi.DTO;
using demoapi.Services.Implementations;
using demoapi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }
    //
    [HttpGet("GetAllWithPagination")]
    public async Task<IActionResult> GetAllGradesWithPagination(int page = 1, int pageSize = 20)
    {
        var result = await _courseService.GetAllCoursesWithPaginationAsync(page, pageSize);
        return Ok(result);
    }

    /*[HttpGet]
    public async Task<IActionResult> GetAllCourses([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _courseService.GetAllCoursesWithPaginationAsync(page, pageSize);

        if (!result.Items.Any())
            return NotFound("No courses found.");

        return Ok(result);
    } */
    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return Ok(courses);
    }
    [HttpGet("filter")]
    public async Task<IActionResult> FilterCourses([FromQuery] string? courseName, [FromQuery] string? description)
    {
        var courses = await _courseService.FilterCoursesAsync(courseName, description);
        if (!courses.Any())
        {
            return NotFound("No courses match the given criteria.");
        }
        return Ok(courses);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
            return NotFound();

        return Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> AddCourse([FromBody] CourseDto courseDto)
    {
        if (courseDto == null)
            return BadRequest("Kurs bilgileri eksik.");

        var addedCourse = await _courseService.AddCourseAsync(courseDto);
        return CreatedAtAction(nameof(GetCourse), new { id = addedCourse.CourseId }, addedCourse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseDto courseDto)
    {
        if (courseDto == null || courseDto.CourseId != id)
            return BadRequest("Geçersiz veri veya ID eþleþmiyor.");

        var updatedCourse = await _courseService.UpdateCourseAsync(courseDto);
        if (updatedCourse == null)
            return NotFound("Kurs bulunamadý.");

        return Ok(updatedCourse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var isDeleted = await _courseService.DeleteCourseAsync(id);
        if (!isDeleted)
            return NotFound("Kurs bulunamadý.");

        return NoContent();
    }
}
