using Home_3.BLL.Models;

namespace Home_3.BLL.interfaces.Repositories;

public interface IAuthorsRepository
{
    ValueTask<IEnumerable<Author>> GetAll();
    ValueTask<Author?> GetById(int id);
    ValueTask<Author> Create(Author author);
    ValueTask<Author?> Update(Author author);
    ValueTask<Author?> Delete(int id);
}