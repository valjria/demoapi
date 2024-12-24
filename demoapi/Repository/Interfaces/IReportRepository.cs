using demoapi.DTO;

namespace demoapi.Repository.Interfaces
{
    namespace demoapi.Repositories.Interfaces
    {
        public interface IReportRepository
        {
            Task<IEnumerable<ReportDto>> GetAllAsync();
        }
    }
}
