using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demoapi.Data;
using demoapi.Models;
using demoapi.DTO;
using AutoMapper;
using demoapi.Services.Interfaces;

namespace demoapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGrades()
        {
            var grades = await _gradeService.GetAllGradesAsync();
            return Ok(grades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrade(int id)
        {
            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
                return NotFound();

            return Ok(grade);
        }

        [HttpPost]
        public async Task<IActionResult> AddGrade([FromBody] GradeDto gradeDto)
        {
            if (gradeDto == null)
                return BadRequest("Not bilgileri eksik.");

            var addedGrade = await _gradeService.AddGradeAsync(gradeDto);
            return CreatedAtAction(nameof(GetGrade), new { id = addedGrade.GradeId }, addedGrade);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var isDeleted = await _gradeService.DeleteGradeAsync(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}