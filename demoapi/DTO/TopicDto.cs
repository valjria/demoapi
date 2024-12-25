namespace demoapi.DTO
{
    public class TopicDto
    {
        public int TopicId { get; set; } // Konunun ID'si
        public string TopicName { get; set; } // Konunun adı
        public int CourseId { get; set; } // İlgili kursun ID'si
        public string CourseName { get; set; } // İlgili kursun adı
    }

}
