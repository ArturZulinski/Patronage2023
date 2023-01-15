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
    public class BookController : Controller
    {
        private readonly BookStoresDBContext _context;
        public BookController(BookStoresDBContext context)
        {
            _context = context;
        }

        [HttpPost("CreateBook")]  //Create book
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.bookId }, book);
        }

        [HttpGet("GetBooks")]
        public IEnumerable<Book> Get() //Read all books
        {
            using (var context = new BookStoreDBContext())
            {
                return context.Book.Tolist();

            }
        }

        [HttpPut("UpdateBook/{id}")]  // Update book by id
        public async Task<IActionResult> PutBook(int id, Book author)
        {
            if (id != book.book_id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("DeleteBook/{id}")] // Delete book by id
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

    }
}
