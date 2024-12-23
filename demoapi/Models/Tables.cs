using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/* namespace MyProject.Models
{
    // Courses Table
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public ICollection<Topic> Topics { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }

    // Topics Table
    public class Topic
    {
        [Key]
        public int TopicId { get; set; }

        [Required]
        [MaxLength(100)]
        public string TopicName { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }


    // Students Table
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } // Kabin/Kokpit

        public ICollection<Grade> Grades { get; set; }
    }

    // Grades Table
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Range(0, 100)]
        public int Value { get; set; } // GradeValue yerine Value
    }


} */
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

    public class Topic
    {
        public int TopicId { get; set; }

        [Required]
        public string TopicName { get; set; }
        public int CourseId { get; set; }

        [JsonIgnore]
        public Course Course { get; set; }
    }

    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } // Kabin/Kokpit


        [JsonIgnore]
        // Navigation Property
        public ICollection<Grade> Grades { get; set; }
    }


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