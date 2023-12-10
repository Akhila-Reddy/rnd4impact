using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using LibraryManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    public class BooksControllerV2 : Controller
    {
        //private readonly LibraryContext _context;
        private readonly IDynamoDBContext _context;

        public BooksControllerV2(IDynamoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        //public async IActionResult GetBooks()
        {
            return await _context.ScanAsync<Book>(default).GetRemainingAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var student = await _context.LoadAsync<Book>(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            var bookObj = await _context.LoadAsync<Book>(book.Id);
            if (bookObj != null) return BadRequest($"Student with Id {book.Id} Already Exists");
            await _context.SaveAsync(book);
            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            var bookReq = await _context.LoadAsync<Book>(id);
            if (bookReq == null) return NotFound();
            await _context.SaveAsync(book);
            return Ok(book);
        }

        //Test-Pool-AWS

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var bookReq = await _context.LoadAsync<Book>(id);
            if (bookReq == null) return NotFound();
            await _context.DeleteAsync(bookReq);
            return NoContent();
        }

        // PUT: api/Books/LendBook/5
        [HttpPut("LendBook/{id}")]
        public async Task<IActionResult> LendBook(int id, bool isBorrowed)
        {
            var book = await _context.LoadAsync<Book>(id);

            if (book == null)
            {
                return NotFound();
            }

            book.IsAvailable = !isBorrowed;
            await _context.SaveAsync(book);

            return NoContent();
        }

        //private bool BookExists(int id)
        //{
         //   return _context.Books.Any(b => b.Id == id);
        //}
    }
}
