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
    public class AuthorsController : Controller
    {
        //private readonly LibraryContext _context;
        private readonly IDynamoDBContext _context;

        public AuthorsController(IDynamoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        //public async IActionResult GetBooks()
        {
            return await _context.ScanAsync<Author>(default).GetRemainingAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var student = await _context.LoadAsync<Author>(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateAuthor(Author author)
        {
            var authorObj = await _context.LoadAsync<Book>(author.Id);
            if (authorObj != null) return BadRequest($"Author with Id {author.Id} Already Exists");
            await _context.SaveAsync(author);
            return Ok(author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthorDetails(int id, Author author)
        {
            var authorReq = await _context.LoadAsync<Author>(id);
            if (authorReq == null) return NotFound();
            await _context.SaveAsync(author);
            return Ok(author);
        }

        //Test-Pool-AWS

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var authorReq = await _context.LoadAsync<Author>(id);
            if (authorReq == null) return NotFound();
            await _context.DeleteAsync(authorReq);
            return NoContent();
        }
    }
}
