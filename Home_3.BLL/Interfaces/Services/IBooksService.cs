using Home_3.BLL.DTOs.Book;
using Home_3.BLL.Models;

namespace Home_3.BLL.interfaces.Services;

public interface IBooksService
{
    Task<IEnumerable<Book>> GetAll();
    Task<Book?> GetById(int id);
    Task<Book> Create(AddBookDTO book);
    Task<Book?> Update(UpdateBookDTO book);
    Task<string?> Delete(int id);
}