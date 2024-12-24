using AutoMapper;
using demoapi.DTO;
using demoapi.Models;
using demoapi.Repositories.Interfaces;
using demoapi.Services.Interfaces;

namespace demoapi.Services.Implementations
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IMapper _mapper;

        public GradeService(IGradeRepository gradeRepository, IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GradeDto>> GetAllGradesAsync()
        {
            var grades = await _gradeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GradeDto>>(grades);
        }

        public async Task<GradeDto> GetGradeByIdAsync(int id)
        {
            var grade = await _gradeRepository.GetByIdAsync(id);
            return grade == null ? null : _mapper.Map<GradeDto>(grade);
        }

        public async Task<GradeDto> AddGradeAsync(GradeDto gradeDto)
        {
            var grade = _mapper.Map<Grade>(gradeDto);
            var addedGrade = await _gradeRepository.AddAsync(grade);
            return _mapper.Map<GradeDto>(addedGrade);
        }

        public async Task<bool> DeleteGradeAsync(int id)
        {
            return await _gradeRepository.DeleteAsync(id);
        }
    }
}