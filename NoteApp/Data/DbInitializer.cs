using NoteApp.Models;

namespace NoteApp.Data
{
    public class DbInitializer
    {
        public const string ADMIN_ID = "admin@example.com";

        private readonly NewsAppContext _context;

        public DbInitializer(NewsAppContext context)
        {
            _context = context;
        }

        public void Run()
        {
            if (_context.Database.EnsureCreated())
            {
                string salt;
                string pwHash = HashGenerator.GenerateHash("user1234", out salt);
                User admin = new User
                { Email = ADMIN_ID, Password = pwHash, Salt = salt };
                // add intial data
                _context.Users.Add(admin);
                _context.News.Add(new News
                {
                    Name = "Trump ist Präsi :-/",
                    Description = "Bla",
                    User = admin
                });
                // store everything to database
                _context.SaveChanges();
            }
        }
    }
}
