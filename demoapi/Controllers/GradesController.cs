using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demoapi.Data;
using demoapi.Models;
using demoapi.DTO;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class GradesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GradesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // Tüm notları listeleme
    [HttpGet]
    public async Task<IActionResult> GetGrades()
    {
        var grades = await _context.Grades
            .Include(g => g.Student) // Öğrenci bilgilerini dahil et
            .Include(g => g.Course)  // Ders bilgilerini dahil et
            .ToListAsync();

        // Grade -> GradeDto dönüşümü
        var gradeDtos = _mapper.Map<List<GradeDto>>(grades);
        return Ok(gradeDtos); // 200 OK ve GradeDto listesi
    }

    // Yeni not ekleme
    [HttpPost]
    public async Task<IActionResult> AddGrade([FromBody] GradeDto gradeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Model doğrulama hatalarını döndür
        }

        // DTO -> Model dönüşümü
        var grade = _mapper.Map<Grade>(gradeDto);

        // İlişkili verileri kontrol et
        var student = await _context.Students.FindAsync(grade.StudentId);
        var course = await _context.Courses.FindAsync(grade.CourseId);

        if (student == null || course == null)
        {
            return BadRequest("Invalid Student or Course."); // Hatalı öğrenci veya ders
        }

        _context.Grades.Add(grade);
        await _context.SaveChangesAsync();

        // Kaydedilen Grade'i DTO'ya çevirip döndür
        var createdGradeDto = _mapper.Map<GradeDto>(grade);
        return CreatedAtAction(nameof(GetGrades), new { id = grade.GradeId }, createdGradeDto); // 201 Created
    }
}
