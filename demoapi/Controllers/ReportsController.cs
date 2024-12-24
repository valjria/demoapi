using demoapi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace demoapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReports()
        {
            var reports = await _reportService.GetAllReportsAsync();
            return Ok(reports);
        }
    }
}