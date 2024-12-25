using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demoapi.Data;
using demoapi.Models;
using demoapi.DTO;
using AutoMapper;
using demoapi.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudent(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        if (student == null)
            return NotFound();

        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> AddStudent([FromBody] StudentDto studentDto)
    {
        var addedStudent = await _studentService.AddStudentAsync(studentDto);
        return CreatedAtAction(nameof(GetStudent), new { id = addedStudent.StudentId }, addedStudent);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentDto studentDto)
    {
       /* if (studentDto == null || studentDto.StudentId != id)
        {
            return BadRequest("Invalid data or mismatched ID.");
        } */

        var updatedStudent = await _studentService.UpdateStudentAsync(studentDto);

        if (updatedStudent == null)
            return NotFound("Student not found.");

        return Ok(updatedStudent);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var isDeleted = await _studentService.DeleteStudentAsync(id);
        if (!isDeleted)
            return NotFound();

        return NoContent();
    }
}
