using AuthApi.Model;
using AuthApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            if (books.Count() > 0)
                return Ok(books);
            return NotFound("Books not found");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateBook([FromBody] BookModel book)
        {
            int result = await _bookRepository.CreateBookAsync(book);
            if (result > 0)
                return Ok(book);
            return BadRequest();
        }
        [HttpPut("")]
        public async Task<IActionResult> UpdateBook([FromBody] BookModel book)
        {
            var result = await _bookRepository.UpdateBookAsync(book);
            if (result != null)
                return Ok(result);
            else
                return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookSingleDBCall(int id, [FromBody] BookModel book)
        {
            var result = await _bookRepository.UpdateBookSingleDBCallAsync(book);
            if (result != null)
                return Ok(result);
            else
                return BadRequest();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBookPatch([FromRoute] int id, [FromBody] JsonPatchDocument book)
        {
            /*
            [HttpPatch] https://localhost:44323/api/books/8
            [
                {
                    "op":"replace",
                    "path":"title",
                    "value":"Updated Title"
                 }
            ]
            */

            var result = await _bookRepository.UpdateBookPatch(book, id);
            if (result != null)
                return Ok(result);
            else
                return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            var result = await _bookRepository.DeleteBookAsync(id);
            if (result)
                return Ok("Book removed successfully");
            return BadRequest();
        }
    }
}
