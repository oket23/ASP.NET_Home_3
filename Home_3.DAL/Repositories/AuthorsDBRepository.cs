using Home_3.BLL.interfaces.Repositories;
using Home_3.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace Home_3.DAL.Repositories;

public class AuthorsDBRepository : IAuthorsRepository
{
    private readonly HomeContext _context;

    public AuthorsDBRepository(HomeContext context)
    {
        _context = context;
    }

    public async ValueTask<IEnumerable<Author>> GetAll()
    {
        return await _context.Authors.AsNoTracking().ToListAsync();
    }

    public async ValueTask<Author?> GetById(int id)
    {
        return await _context.Authors.FindAsync(id); 
    }
    public async ValueTask<Author> Create(Author author)
    {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
        return author;
    }

    public async ValueTask<Author?> Update(Author author)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
        return author;
    }

    public async ValueTask<Author?> Delete(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            return null;
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return author;
    }
}