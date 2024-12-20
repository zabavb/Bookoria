﻿namespace BookApi.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAuthorsAsync();
        Task<AuthorDto> GetAuthorByIdAsync(Guid id);
        Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto);
        Task<AuthorDto> UpdateAuthorAsync(Guid id, AuthorDto authorDto);
        Task<bool> DeleteAuthorAsync(Guid id);
    }
}
