﻿
namespace BookApi.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetBooksAsync();
        Task<BookDto> GetBookByIdAsync(Guid bookId);
        Task<BookDto> CreateBookAsync(BookDto bookDto);
        Task<BookDto> UpdateBookAsync(Guid id, BookDto bookDto);
        Task<bool> DeleteBookAsync(Guid id);


    }
}