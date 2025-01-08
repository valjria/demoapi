using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace demoapi.Models
{
    public class Topic
    {
        public int TopicId { get; set; }

        [Required]
        public string TopicName { get; set; }
        public int CourseId { get; set; }

        [JsonIgnore]
        public Course Course { get; set; }
    }
}
