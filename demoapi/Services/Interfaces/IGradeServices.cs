using demoapi.DTO;

namespace demoapi.Services.Interfaces
{
    public interface IGradeService
    {
        Task<IEnumerable<GradeDto>> GetAllGradesAsync();
        Task<GradeDto> GetGradeByIdAsync(int id);
        Task<GradeDto> AddGradeAsync(GradeDto gradeDto);
        Task<GradeDto> UpdateGradeAsync(GradeDto gradeDto);
        Task<bool> DeleteGradeAsync(int id);
        Task<IEnumerable<GradeDto>> FilterGradesAsync(int? studentId, int? courseId, int? minValue, int? maxValue);
        Task<PaginationDto<GradeDto>> GetAllGradesWithPaginationAsync(int page, int pageSize);

    }
}