using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demoapi.Data;
using demoapi.DTO;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly EducationDbContext _context;
    private readonly IMapper _mapper;

    public ReportsController(EducationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudentReport()
    {
        var grades = await _context.Grades
            .Include(g => g.Student)
            .Include(g => g.Course)
            .ToListAsync();

        // Grade listesini StudentReportDto'ya mapliyoruz
        var reportDtos = _mapper.Map<List<ReportDto>>(grades);

        return Ok(reportDtos);
    }
}
