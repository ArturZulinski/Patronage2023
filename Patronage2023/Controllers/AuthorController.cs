using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Web;
using System.Web.Mvc;

namespace Patronage2023.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        private readonly BookStoresDBContext _context;
        public AuthorController(BookStoresDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetAuthors")]
        public IEnumerable<Author> Get() //READ ALL AUTHORS
        {
            using (var context = new BookStoreDBContext())
            {
                return context.Author.Tolist();

            }
        }

        [HttpGet("GetAuthor/{last_name}")] // Author by name
        public async Task<ActionResult<Author>> GetAuthor(char last_name)
        {
            var author = await _context.Author.FindAsync(last_name);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        [HttpPost("CreateAuthor")]  //Create author
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.author_id }, author);
        }

    }
}
