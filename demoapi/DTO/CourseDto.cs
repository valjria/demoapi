namespace demoapi.DTO
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public List<TopicDto> Topics { get; set; }
    }

}
