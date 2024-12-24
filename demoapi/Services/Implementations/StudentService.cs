using demoapi.DTO;
using demoapi.Models;
using demoapi.Repository.Interfaces;
using demoapi.Services.Interfaces;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
    {
        var students = await _studentRepository.GetAllAsync();
        return students.Select(student => new StudentDto
        {
            StudentId = student.StudentId,
            Name = student.Name,
            Role = student.Role
        });
    }

    public async Task<StudentDto> GetStudentByIdAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student == null) return null;

        return new StudentDto
        {
            StudentId = student.StudentId,
            Name = student.Name,
            Role = student.Role
        };
    }

    public async Task<StudentDto> AddStudentAsync(StudentDto studentDto)
    {
        var student = new Student
        {
            Name = studentDto.Name,
            Role = studentDto.Role
        };

        var addedStudent = await _studentRepository.AddAsync(student);

        return new StudentDto
        {
            StudentId = addedStudent.StudentId,
            Name = addedStudent.Name,
            Role = addedStudent.Role
        };
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        return await _studentRepository.DeleteAsync(id);
    }

    public Task<StudentDto> UpdateStudentAsync(StudentDto studentDto)
    {
        throw new NotImplementedException();
    }
}