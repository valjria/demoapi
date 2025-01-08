using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace demoapi.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; } // Primary Key

        [Required] 
        [MaxLength(100)] 
        [Column(TypeName = "varchar(100)")] 
        public string Name { get; set; }

        [Required] 
        [MaxLength(50)] 
        [Column(TypeName = "varchar(50)")] // 
        public string Role { get; set; } 

        [JsonIgnore]
        public ICollection<Grade> Grades { get; set; } 
    }
}
