using demoapi.DTO;

namespace demoapi.Services.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDto>> GetAllReportsAsync();
    }
}
