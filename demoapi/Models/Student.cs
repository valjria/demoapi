using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace demoapi.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        //[Required]
        [MaxLength(100)]
        public string Name { get; set; }

       // [Required]
        [MaxLength(50)]
        public string Role { get; set; } // Kabin/Kokpit


        [JsonIgnore]
        // Navigation Property
        public ICollection<Grade> Grades { get; set; }
    }
}
