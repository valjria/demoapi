using AutoMapper;
using demoapi.Models;
using demoapi.DTO;

namespace demoapi.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Course mapping
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => src.Topics));

            CreateMap<CourseDto, Course>()
             .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => src.Topics));

            //Topic mapping
            CreateMap<Topic, TopicDto>();
            CreateMap<TopicDto, Topic>();

            //Student mapping
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();

            //Grade mapping
            CreateMap<Grade, GradeDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.CourseName));

            CreateMap<GradeDto, Grade>();

            //Report mapping
            CreateMap<Grade, ReportDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.CourseName))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Value));
        }

    }
    }

