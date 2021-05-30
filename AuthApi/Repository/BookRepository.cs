using AuthApi.Model;
using DBAccess;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        public BookRepository(BookStoreContext context)
        {
            this._context = context;
        }
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            var records = await _context.books.
                Select(x => new BookModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Title = x.Title,
                    DatePublished = x.DatePublished
                })
                .ToListAsync();
            return records;
        }
        public async Task<BookModel> GetBookByIdAsync(int Id)
        {
            var record = await _context.books
                .Select(book => new BookModel()
                {
                    Id = book.Id,
                    Name = book.Name,
                    Title = book.Title,
                    DatePublished = book.DatePublished
                })
                .FirstOrDefaultAsync(b => b.Id == Id);

            return record;
        }
        public async Task<int> CreateBookAsync(BookModel book)
        {
            DBAccess.Models.BookModel bookM = new DBAccess.Models.BookModel()
            {
                Id = book.Id,
                Name = book.Name,
                Title = book.Title,
                DatePublished = DateTime.Now
            };
            _context.books.Add(bookM);
            int result = await _context.SaveChangesAsync();
            book.Id = bookM.Id;
            book.DatePublished = bookM.DatePublished;
            return result;
        }
        public async Task<BookModel> UpdateBookAsync(BookModel book)
        {
            var bookRepo = _context.books
                .Where(x => x.Id == book.Id).FirstOrDefault();

            bookRepo.Name = book.Name;
            bookRepo.Title = book.Title;
            bookRepo.DatePublished = book.DatePublished;

            int result = await _context.SaveChangesAsync();
            if (result > 0)
                return book;
            else
                return null;
        }
        public async Task<BookModel> UpdateBookSingleDBCallAsync(BookModel book)
        {
            DBAccess.Models.BookModel bookM = new DBAccess.Models.BookModel()
            {
                Id = book.Id,
                Name = book.Name,
                Title = book.Title,
                DatePublished = DateTime.Now
            };
            _context.books.Update(bookM);
            int result = await _context.SaveChangesAsync();
            if (result > 0)
                return book;
            else
                return null;
        }
        public async Task<BookModel> UpdateBookPatch(JsonPatchDocument book, int id)
        {
            var res = await _context.books.FindAsync(id);
            if (book != null)
            {
                book.ApplyTo(res);
                await _context.SaveChangesAsync();
            }
            return new BookModel()
            {
                Id = res.Id,
                Name = res.Name,
                Title = res.Title,
                DatePublished = res.DatePublished
            };
        }
        public async Task<bool> DeleteBookAsync(int Id)
        {
            var book = new DBAccess.Models.BookModel() { Id = Id };
            _context.Remove(book);
            int result = await _context.SaveChangesAsync();
            return result > 0 ? true : false;
        }

    }
}
