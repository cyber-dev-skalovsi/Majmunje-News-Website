using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteApp.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using NoteApp.Data;
namespace NoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly NewsAppContext _context;

        public NewsController(NewsAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/notes/5
        /// </summary>
        /// <example>
        ///  <code>
        ///  async function getNote() {
        ///    const request = await fetch('http://localhost:4200/api/news/1', {
        ///      headers: {
        ///        'Accept': 'application/json'
        ///        },
        ///      method: 'GET'
        ///    });
        ///    const data = await request.json();
        ///    console.log(data);
        ///  }
        ///  </code>
        /// </example>
        /// <param name="id">ID of the note to retrieve.</param>
        /// <returns>Returns the note associated to the given id.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var toGet = _context.News.Find(id);

            if (toGet != null)
            {
                return Ok(toGet);
            }
            return NotFound();
        }

        /// <summary>
        /// GET: api/notes
        /// </summary>
        /// <example>
        ///  <code>
        ///  async function getNotes() {
        ///    const request = await fetch('http://localhost:4200/api/news/', {
        ///      headers: {
        ///        'Accept': 'application/json'
        ///        },
        ///      method: 'GET'
        ///    });
        ///    const data = await request.json();
        ///    console.log(data);
        ///  }
        ///  </code>
        /// </example>
        /// <returns>Returns all notes in the database.</returns>
        [HttpGet()]
        public IActionResult GetAll()
        {
            return Ok(_context.News);
        }

        /// <summary>
        /// POST: api/notes
        /// </summary>
        /// <example>
        ///  <code>
        ///  async function addNote() {
        ///    const request = await fetch('http://localhost:4200/api/notes/', {
        ///      headers: {
        ///        'Accept': 'application/json',
        ///        'Content-Type': 'application/json'
        ///        },
        ///      method: 'POST',
        ///      body: JSON.stringify({ todo: 'new todo', dueDate: '2023-08-19T18:31:35.294Z', completionDate: '2023-08-19T18:31:35.294Z' }),
        ///    });
        ///    const data = await request.json();
        ///    console.log(data);
        ///  }
        ///  </code>
        /// </example>
        /// <param name="note">Note data to be stored.</param>
        /// <returns>Returns the created note.</returns>
        [HttpPost()]
        [Authorize]
        public IActionResult Post(News news)
        {

            Claim subClaim = User.Claims.First(
             claim => claim.Type == ClaimTypes.NameIdentifier);
            long userId = Convert.ToInt64(subClaim.Value);
            news.UserId = userId;
            if (_context.Users.Find(userId) == null)
            {
                return BadRequest();
            }
            if (_context.Users.Find(userId).Email != DbInitializer.ADMIN_ID) //Authentication problem
            {
                return Unauthorized("You do not have permission to perform this action.");
            }
            var newNote = _context.News.Add(news);
            _context.SaveChanges();
            return Ok(newNote.Entity);
        }

        /// <summary>
        /// PUT: api/notes/5
        /// </summary>
        /// <example>
        ///  <code>
        ///  async function updateNote() {
        ///    const request = await fetch('http://localhost:4200/api/notes/1', {
        ///      headers: {
        ///        'Accept': 'application/json',
        ///        'Content-Type': 'application/json'
        ///        },
        ///      method: 'PUT',
        ///      body: JSON.stringify({ id: 1, todo: 'overwrite todo', dueDate: '2023-08-20T18:31:35.294Z', completionDate: '2023-08-20T18:31:35.294Z' }),
        ///    });
        ///    const data = request.status;
        ///    console.log(data);
        ///  }
        ///  </code>
        /// </example>
        /// <param name="id">ID of the note to be edited.</param>
        /// <param name="note">Note data to be stored.</param>
        /// <returns>Nothing, see HTTP status code.</returns>
        [HttpPut("{id}")]
        // For LINQ methods like FirstOrDefault

        [Authorize] // Ensures the user is authenticated
        public IActionResult Put([FromRoute] long id, [FromBody] News note)
        {
            // Find the news entry to edit
            var toEdit = _context.News.Find(id);
            if (toEdit != null)
            {
                // Update the fields
                toEdit.PictureUrl = note.PictureUrl;
                toEdit.Name = note.Name;
                toEdit.Description = note.Description;

                // Save changes
                _context.SaveChanges();
                return Ok();
            }

            // Return NotFound if the entry is not found
            return NotFound();
        }
    }
}