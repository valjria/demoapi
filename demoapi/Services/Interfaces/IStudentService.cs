using demoapi.DTO;

namespace demoapi.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto> GetStudentByIdAsync(int id);
        Task<StudentDto> AddStudentAsync(StudentDto studentDto);
        Task<StudentDto> UpdateStudentAsync(StudentDto studentDto);
        Task<bool> DeleteStudentAsync(int id);
    }

}
