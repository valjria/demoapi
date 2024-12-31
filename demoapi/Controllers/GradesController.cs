﻿using demoapi.DTO;
using demoapi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("GetAllWithPagination")]
        public async Task<IActionResult> GetAllGradesWithPagination(int page = 1, int pageSize = 20)
        {
            var result = await _gradeService.GetAllGradesWithPaginationAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetGrades()
        {
            var grades = await _gradeService.GetAllGradesAsync();
            return Ok(grades);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> FilterGrades([FromQuery] int? studentId, [FromQuery] int? courseId, [FromQuery] int? minValue, [FromQuery] int? maxValue)
        {
            var grades = await _gradeService.FilterGradesAsync(studentId, courseId, minValue, maxValue);
            if (!grades.Any())
            {
                return NotFound("No grades match the given criteria.");
            }
            return Ok(grades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrade(int id)
        {
            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null) return NotFound();

            return Ok(grade);
        }

        [HttpPost]
        public async Task<IActionResult> AddGrade([FromBody] GradeDto gradeDto)
        {
            if (gradeDto == null) return BadRequest("Not bilgileri eksik.");

            var addedGrade = await _gradeService.AddGradeAsync(gradeDto);
            return CreatedAtAction(nameof(GetGrade), new { id = addedGrade.GradeId }, addedGrade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrade(int id, [FromBody] GradeDto gradeDto)
        {
            if (gradeDto == null || gradeDto.GradeId != id)
                return BadRequest("Geçersiz veri veya ID eşleşmiyor.");

            var updatedGrade = await _gradeService.UpdateGradeAsync(gradeDto);
            if (updatedGrade == null) return NotFound("Not bulunamadı.");

            return Ok(updatedGrade);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var isDeleted = await _gradeService.DeleteGradeAsync(id);
            if (!isDeleted) return NotFound("Not bulunamadı.");

            return NoContent();
        }
    }
}