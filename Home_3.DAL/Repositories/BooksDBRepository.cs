using Home_3.BLL.interfaces.Repositories;
using Home_3.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace Home_3.DAL.Repositories;

public class BooksDBRepository : IBooksRepository
{
    private readonly HomeContext _context;

    public BooksDBRepository(HomeContext context)
    {
        _context = context;
    }

    public async ValueTask<IEnumerable<Book>> GetAll()
    {
        return await _context.Books
            .AsNoTracking()
            .Include(b => b.Author) 
            .ToListAsync();
    }

    public async ValueTask<Book?> GetById(int id)
    {
        return await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async ValueTask<Book> Create(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async ValueTask<Book?> Update(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async ValueTask<Book?> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return null;
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return book;
    }
    
    public async ValueTask<IEnumerable<Book>> GetByAuthorId(int authorId)
    {
        return await _context.Books
            .AsNoTracking()
            .Where(b => b.AuthorId == authorId)
            .Include(b => b.Author)
            .ToListAsync();
    }
}