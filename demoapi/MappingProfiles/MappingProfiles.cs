using AutoMapper;
using demoapi.Models;
using demoapi.DTO;

namespace demoapi.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Mapping for Course
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => src.Topics));

            CreateMap<CourseDto, Course>();

            // Mapping for Topic
            CreateMap<Topic, TopicDto>();
            CreateMap<TopicDto, Topic>();

            // Mapping for Student
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();

            // Mapping for Grade
            CreateMap<Grade, GradeDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.CourseName));

            CreateMap<GradeDto, Grade>();

            CreateMap<Grade, ReportDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.CourseName))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Value));

        }
    }
}
