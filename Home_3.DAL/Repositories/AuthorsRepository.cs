using Home_3.BLL.interfaces.Repositories;
using Home_3.BLL.Models;

namespace Home_3.DAL.Repositories;

public class AuthorsRepository : IAuthorsRepository
{
    private readonly List<Author> _authors = new();
    private int _randomId;

    public AuthorsRepository()
    {
        _randomId = 1;
    }
    public ValueTask<IEnumerable<Author>> GetAll()
    {
        return new ValueTask<IEnumerable<Author>>(_authors);
    }

    public ValueTask<Author?> GetById(int id)
    {
        var author = _authors.FirstOrDefault(a => a.Id == id);
        return new ValueTask<Author?>(author);
    }

    public ValueTask<Author> Create(Author author)
    {
        author.Id = GetRandomId();
        _authors.Add(author);
        
        return new ValueTask<Author>(author);
    }

    public ValueTask<Author?> Update(Author author)
    {
        var authorToUpdate = _authors.FirstOrDefault(a => a.Id == author.Id);

        if (authorToUpdate is null)
        {
            return new ValueTask<Author?>(result: null);
        }

        authorToUpdate.FirstName = author.FirstName;
        authorToUpdate.LastName = author.LastName;
        authorToUpdate.BirthdayDate = author.BirthdayDate;

        return new ValueTask<Author?>(authorToUpdate);
    }

    public ValueTask<Author?> Delete(int id)
    {
        var authorToDelete = _authors.FirstOrDefault(x => x.Id == id);

        if (authorToDelete is null)
        {
            return new ValueTask<Author?>(result: null);
        }

        _authors.Remove(authorToDelete);
        return new ValueTask<Author?>(authorToDelete);
    }
    
    private int GetRandomId()
    {
        return _randomId++;
    }
}