using Home_3.BLL.Models;

namespace Home_3.BLL.interfaces.Repositories;

public interface IBooksRepository
{
    ValueTask<IEnumerable<Book>> GetAll();
    ValueTask<Book?> GetById(int id);
    ValueTask<Book> Create(Book book);
    ValueTask<Book?> Update(Book book);
    ValueTask<Book?> Delete(int id);
    ValueTask<IEnumerable<Book>> GetByAuthorId(int authorId);
}
