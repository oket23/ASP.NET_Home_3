using Home_3.BLL.interfaces.Repositories;
using Home_3.BLL.Models;

namespace Home_3.DAL.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly List<Book> _books = new();
    private int _randomId;

    public BooksRepository()
    {
        _randomId = 1;
    }

    public ValueTask<IEnumerable<Book>> GetAll()
    {
        return new ValueTask<IEnumerable<Book>>(_books);
    }

    public ValueTask<Book?> GetById(int id)
    {
        var book = _books.FirstOrDefault(a => a.Id == id);
        return new ValueTask<Book?>(book);
    }

    public ValueTask<Book>  Create(Book book)
    {
        book.Id = GetRandomId();
        _books.Add(book);
        
        return new ValueTask<Book>(book);
    }

    public ValueTask<Book?> Update(Book book)
    {
        var bookToUpdate = _books.FirstOrDefault(a => a.Id == book.Id);

        if (bookToUpdate is null)
        {
            return new ValueTask<Book?>(result: null);
        }
        
        bookToUpdate.Name = book.Name;
        bookToUpdate.Description = book.Description;
        bookToUpdate.PublicationYear = book.PublicationYear;
        bookToUpdate.AuthorId = book.AuthorId;

        return new ValueTask<Book?>(bookToUpdate);
    }

    public ValueTask<Book?> Delete(int id)
    {
        var bookToDelete = _books.FirstOrDefault(x => x.Id == id);

        if (bookToDelete is null)
        {
            return new ValueTask<Book?>(result: null);
        }

        _books.Remove(bookToDelete);
        return new ValueTask<Book?>(bookToDelete);
    }
    
    public ValueTask<IEnumerable<Book>>  GetByAuthorId(int authorId)
    {
        var result = _books.Where(b => b.AuthorId == authorId).ToList();
        return new ValueTask<IEnumerable<Book>>(result);
    }
    
    private int GetRandomId()
    {
        return _randomId++;
    }
}