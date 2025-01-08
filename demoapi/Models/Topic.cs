using demoapi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Topic
{
    [Key]
    public int TopicId { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string TopicName { get; set; }

    [ForeignKey("Course")]
    public int CourseId { get; set; }

    [JsonIgnore]
    public Course Course { get; set; }
}
