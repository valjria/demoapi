using demoapi.DTO;

namespace demoapi.Services.Interfaces
{
    public interface IGradeService
    {
        Task<IEnumerable<GradeDto>> GetAllGradesAsync();
        Task<GradeDto> GetGradeByIdAsync(int id);
        Task<GradeDto> AddGradeAsync(GradeDto gradeDto);
        Task<bool> DeleteGradeAsync(int id);
    }
}