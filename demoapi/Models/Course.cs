using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace demoapi.Models
{
    public class Course
    {

        public int CourseId { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string CourseName { get; set; }
        [Column(TypeName = "text")]
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Grade> Grades { get; set; } 

    }
}
