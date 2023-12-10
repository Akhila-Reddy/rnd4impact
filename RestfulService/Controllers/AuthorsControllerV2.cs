using System.Collections.Generic;
using System.Threading.Tasks;
using global::LibraryManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LibraryManagementSystem.Controllers
{

    namespace LibraryManagementSystem.Controllers
    {
        [Route("api/[controller]")]
        public class AuthorsControllerV2 : Controller
        {
            private readonly LibraryContext _context;
            //private readonly IDynamoDBContext _context;

            public AuthorsControllerV2(LibraryContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
            //public async IActionResult GetBooks()
            {
                return await _context.Authors.Include<Author>(default).ToListAsync();
            }


            [HttpGet("{id}")]
            public async Task<ActionResult<Author>> GetAuthor(int id)
            {
                var student = await _context.Authors.Include(b => b.Id).FirstOrDefaultAsync(b => b.Id == id);
                if (student == null) return NotFound();
                return Ok(student);
            }

            [HttpPost]
            public async Task<ActionResult<Book>> CreateAuthor(Author author)
            {
                var authorObj = await _context.Authors.Include(b => b.Id).FirstOrDefaultAsync(b => b.Id == author.Id);
                if (authorObj != null) return BadRequest($"Author with Id {author.Id} Already Exists");
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
                return Ok(author);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateAuthorDetails(int id, Author author)
            {
                var authorReq = await _context.Authors.Include(b => b.Id).FirstOrDefaultAsync(b => b.Id == id);
                if (authorReq == null) return NotFound();
                await _context.SaveChangesAsync();
                return Ok(author);
            }

            //Test-Pool-AWS

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteAuthor(int id)
            {
                var authorReq = await _context.Authors.Include(b => b.Id).FirstOrDefaultAsync(b => b.Id == id);
                if (authorReq == null) return NotFound();    
                _context.Authors.Remove(authorReq);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }

}

