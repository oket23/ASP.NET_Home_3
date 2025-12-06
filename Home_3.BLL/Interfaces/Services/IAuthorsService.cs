using Home_3.BLL.DTOs.Author;
using Home_3.BLL.Models;

namespace Home_3.BLL.interfaces.Services;

public interface IAuthorsService
{
    Task<IEnumerable<Author>> GetAll();
    Task<Author?> GetById(int id);
    Task<Author> Create(AddAuthorDTO book);
    Task<Author?> Update(UpdateAuthorDTO book);
    Task<string?> Delete(int id);
    Task<AuthorWithBooksDTO?> GetByIdWithBooks(int id);
}