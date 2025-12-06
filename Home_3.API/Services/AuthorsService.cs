using Home_3.BLL.DTOs.Author;
using Home_3.BLL.interfaces.Repositories;
using Home_3.BLL.interfaces.Services;
using Home_3.BLL.Models;

namespace Home_3.Services;

public class AuthorsService : IAuthorsService
{
    private readonly IAuthorsRepository _authorsRepository;
    private readonly IBooksRepository  _booksRepository;
    private int _randomId;

    public AuthorsService(IAuthorsRepository authorsRepository, IBooksRepository booksRepository)
    {
        _authorsRepository = authorsRepository;
        _booksRepository = booksRepository;
        _randomId = 1;
    }

    public async Task<IEnumerable<Author>> GetAll()
    {
        return await _authorsRepository.GetAll();
    }

    public async Task<Author?> GetById(int id)
    {
        return await _authorsRepository.GetById(id);
    }

    public async Task<Author> Create(AddAuthorDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FirstName))
        {
            throw new ArgumentException("First name is required.");
        }
        
        if (string.IsNullOrWhiteSpace(dto.LastName))
        {
            throw new ArgumentException("Last name is required.");
        }

        var author = new Author
        {
            Id = GetNextId(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            BirthdayDate = dto.BirthdayDate
        };

        return await _authorsRepository.Create(author);
    }

    public async Task<Author?> Update(UpdateAuthorDTO dto)
    {
        var author = await _authorsRepository.GetById(dto.Id);

        if (author is null)
        {
            return null;
        }

        var updated = new Author
        {
            Id = author.Id,
            FirstName = dto.FirstName ?? author.FirstName,
            LastName = dto.LastName ?? author.LastName,
            BirthdayDate = dto.BirthdayDate ?? author.BirthdayDate
        };

        return await _authorsRepository.Update(updated);
    }

    public async Task<string?> Delete(int id)
    {
        var deleted = await _authorsRepository.Delete(id);

        if (deleted is null)
        {
            return null;
        }

        return $"Deleted author id:{deleted.Id}";
    }

    private int GetNextId()
    {
        return _randomId++;
    }
    
    public async Task<AuthorWithBooksDTO?> GetByIdWithBooks(int id)
    {
        var author = await _authorsRepository.GetById(id);
        
        if (author is null)
        {
            return null;
        }
        
        var books = await _booksRepository.GetByAuthorId(id);
        
        var result = new AuthorWithBooksDTO
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            BirthdayDate = author.BirthdayDate,
            Books = books.ToList()
        };

        return result;
    }
}
