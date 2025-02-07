
namespace NoteApp.Models
{
    public class User
    {
        public long Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }

        public ICollection<News> News { get; set; }
    }
}