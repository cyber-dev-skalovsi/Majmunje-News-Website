using System.Text.Json.Serialization;
namespace NoteApp.Models
{
    public class News
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }

        [JsonIgnore] public long UserId { get; set; }
        [JsonIgnore] public virtual User? User { get; set; }
    }
}
