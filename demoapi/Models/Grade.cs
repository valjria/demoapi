using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace demoapi.Models
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [JsonIgnore]

        public Student Student { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        [JsonIgnore]
        public Course Course { get; set; }

        [Range(0, 100)]
        public int Value { get; set; }
    }
}
