using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Taskapp.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Event Titel is required")]
        [StringLength(200, ErrorMessage = "Title can't be longer than 200 characters")] 
        public string Title { get; set; }

        public string Type { get; set; }

        public string Author { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string? Imagepath { get; set; }
        public DateTime Ts { get; set; }

        public bool Status { get; set; }

        [DisplayName("Published Date")]
        public DateTime Datepublished { get; set; }
    }
}
