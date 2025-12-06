using Home_3.BLL.DTOs.Book;
using Home_3.BLL.interfaces.Repositories;
using Home_3.BLL.interfaces.Services;
using Home_3.BLL.Models;

namespace Home_3.Services;

public class BooksService : IBooksService
{
    private readonly IBooksRepository _booksRepository;
    private readonly IValidationService _validationService;

    public BooksService(
        IBooksRepository repository, 
        IValidationService validationService)
    {
        _booksRepository = repository;
        _validationService = validationService;
    }
    
    public async Task<IEnumerable<Book>> GetAll()
    {
        return await _booksRepository.GetAll();
    }

    public async Task<Book?> GetById(int id)
    {
        return await _booksRepository.GetById(id);
    }

    public async Task<Book> Create(AddBookDTO addBookDto)
    {
        if (!_validationService.IsValidAuthor(addBookDto.AuthorId))
        {
            throw new ArgumentException("Author with given id does not exist.");
        }
       
        if (!_validationService.IsValidDescription(addBookDto.Description))
        {
            throw new ArgumentException("Description contains censored words or is invalid.");
        }

        var book = new Book
        {
            Name = addBookDto.Name,
            PublicationYear = addBookDto.PublicationYear,
            AuthorId = addBookDto.AuthorId,
            Description = addBookDto.Description,
        };
        
        return await _booksRepository.Create(book);
    }

    public async Task<Book?> Update(UpdateBookDTO updateBookDto)
    {
        var book = await _booksRepository.GetById(updateBookDto.Id);
        
        if (book == null)
        {
            return null;
        }
        
        var newAuthorId = updateBookDto.AuthorId ?? book.AuthorId;
        var newDescription = updateBookDto.Description ?? book.Description;
        
        if (!_validationService.IsValidAuthor(newAuthorId))
        {
            throw new ArgumentException("Author with given id does not exist.");
        }
        
        if (!_validationService.IsValidDescription(newDescription))
        {
            throw new ArgumentException("Description contains censored words or is invalid.");
        }

        var updatedBook = new Book
        {
            Id = book.Id,
            Name = updateBookDto.Name ?? book.Name,
            PublicationYear = updateBookDto.PublicationYear ?? book.PublicationYear,
            AuthorId = newAuthorId,
            Description = newDescription,
        };

        return await _booksRepository.Update(updatedBook);
    }

    public async Task<string?> Delete(int id)
    {
        var book = await _booksRepository.Delete(id);
        if (book == null)
        {
            return null;
        }

        return $"Deleted book id:{book.Id}";
    }
}
