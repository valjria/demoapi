using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace demoapi.Models
{
    public class Course
    {
        
            public int CourseId { get; set; }

            [Required]
            public string CourseName { get; set; }

            public string Description { get; set; }

            [JsonIgnore]
            public ICollection<Topic> Topics { get; set; }
            public ICollection<Grade> Grades { get; set; } // İlgili Grades

        
    }
}
