using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demoapi.Data;
using demoapi.Models;
using demoapi.DTO;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly EducationDbContext _context;
    private readonly IMapper _mapper;

    public StudentsController(EducationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // Tüm öğrencileri listeleme
    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        var students = await _context.Students.ToListAsync();

        // Öğrenci listesini DTO'ya mapliyoruz
        var studentDtos = _mapper.Map<List<StudentDto>>(students);

        return Ok(studentDtos);
    }

    // Yeni öğrenci ekleme
    [HttpPost]
    public async Task<IActionResult> AddStudent([FromBody] StudentDto studentDto)
    {
        if (studentDto == null)
        {
            return BadRequest("Öğrenci bilgileri eksik.");
        }

        // DTO'dan modele dönüşüm
        var student = _mapper.Map<Student>(studentDto);

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        // Eklenen öğrenciyi DTO olarak döndür
        var createdStudentDto = _mapper.Map<StudentDto>(student);

        return CreatedAtAction(nameof(GetStudents), new { id = student.StudentId }, createdStudentDto);
    }
}
