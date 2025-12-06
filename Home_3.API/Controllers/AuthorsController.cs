using Home_3.BLL.DTOs.Author;
using Home_3.BLL.interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Home_3.Controllers;

[ApiController]
[Route("v1/authors")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorsService _authorsService;

    public AuthorsController(IAuthorsService authorsService)
    {
        _authorsService = authorsService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var  authors = await _authorsService.GetAll();
            return  authors.Any() ? Ok( authors) : NotFound("No authors found");
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
            var author = await _authorsService.GetByIdWithBooks(id);
            
            if (author == null)
            {
                return NotFound($"Author with id {id} not found");
            }

            return Ok(author);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddAuthorDTO addAuthorDto)
    {
        try
        {
            var createdAuthor = await _authorsService.Create(addAuthorDto);
            
            return Ok(createdAuthor);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateAuthorDTO authorDto)
    {
        try
        {
            if (id != authorDto.Id)
            {
                return BadRequest("Route id does not match author id");
            }
            
            var updatedAuthor = await _authorsService.Update(authorDto);
            
            if (updatedAuthor == null)
            {
                return NotFound($"Author with id {id} not found for update");
            }
            
            return Ok(updatedAuthor);
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
            var deletedAuthor = await _authorsService.Delete(id);
            
            if (deletedAuthor == null)
            {
                return NotFound($"Author with id {id} not found");
            }
            
            return Ok(deletedAuthor);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}