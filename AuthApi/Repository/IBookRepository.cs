using AuthApi.Model;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookByIdAsync(int Id);
        Task<int> CreateBookAsync(BookModel book);
        Task<BookModel> UpdateBookAsync(BookModel book);
        Task<BookModel> UpdateBookSingleDBCallAsync(BookModel book);
        Task<BookModel> UpdateBookPatch(JsonPatchDocument book,int id);
        Task<bool> DeleteBookAsync(int Id);

    }
}
