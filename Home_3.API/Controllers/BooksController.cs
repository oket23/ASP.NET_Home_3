using Home_3.BLL.DTOs.Book;
using Home_3.BLL.interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Home_3.Controllers;

[ApiController]
[Route("v1/books")]
public class BooksController : ControllerBase
{
    private readonly IBooksService _bookService;

    public BooksController(IBooksService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var books = await _bookService.GetAll();
            return books.Any() ? Ok(books) : NotFound("No books found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var book = await _bookService.GetById(id);
            
            if (book == null)
            {
                return NotFound($"Book with id {id} not found");
            }

            return Ok(book);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddBookDTO addBookDto)
    {
        try
        {
            var createdBook = await _bookService.Create(addBookDto);
            
            return Ok(createdBook);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateBookDTO updateBookDto)
    {
        try
        {
            if (id != updateBookDto.Id)
            {
                return BadRequest("Route id does not match book id");
            }
            
            var updatedBook = await _bookService.Update(updateBookDto);
            
            if (updatedBook == null)
            {
                return NotFound($"Book with id {id} not found for update");
            }
            
            return Ok(updatedBook);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        try
        {
            var deletedBook = await _bookService.Delete(id);
            
            if (deletedBook == null)
            {
                return NotFound($"Book with id {id} not found");
            }
            
            return Ok(deletedBook);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}