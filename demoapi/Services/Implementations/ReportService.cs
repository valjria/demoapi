using demoapi.DTO;
using demoapi.Repository.Interfaces.demoapi.Repositories.Interfaces;
using demoapi.Services.Interfaces;

namespace demoapi.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<IEnumerable<ReportDto>> GetAllReportsAsync()
        {
            return await _reportRepository.GetAllAsync();
        }
    }
}