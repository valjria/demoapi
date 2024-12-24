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
            .Include(g => g.Student)
            .Include(g => g.Course)
            .ToListAsync();

        // Grade listesi DTO'ya mapleniyor
        var gradeDtos = _mapper.Map<List<GradeDto>>(grades);
        return Ok(gradeDtos);
    }

    // Yeni not ekleme
    [HttpPost]
    public async Task<IActionResult> AddGrade([FromBody] GradeDto gradeDto)
    {
        if (gradeDto == null)
        {
            return BadRequest("Grade details missing.");
        }

        // DTO'dan Grade modeline mapleme
        var grade = _mapper.Map<Grade>(gradeDto);

        // Doğru öğrenci ve ders var mı kontrol et
        var student = await _context.Students.FindAsync(grade.StudentId);
        var course = await _context.Courses.FindAsync(grade.CourseId);

        if (student == null || course == null)
        {
            return BadRequest("Invalid Student or Course.");
        }

        // StudentName ve CourseName'i doldur
        gradeDto.StudentName = student.Name;
        gradeDto.CourseName = course.CourseName;

        _context.Grades.Add(grade);
        await _context.SaveChangesAsync();

        // Oluşturulan Grade'i DTO'ya mapleyip döndürüyoruz
        var createdGradeDto = _mapper.Map<GradeDto>(grade);
        return CreatedAtAction(nameof(GetGrades), new { id = grade.GradeId }, createdGradeDto);
    }
}
