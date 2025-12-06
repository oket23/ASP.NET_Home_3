using System.Text.RegularExpressions;
using Home_3.BLL.interfaces.Repositories;
using Home_3.BLL.interfaces.Services;
using Home_3.BLL.Models;

namespace Home_3.Services;

public class ValidationService : IValidationService
{
    private readonly IAuthorsRepository _authorsRepository;
    private static readonly string[] _censoredWords = {"the", "no"};
    public ValidationService(IAuthorsRepository authorsRepository)
    {
        _authorsRepository = authorsRepository;
    }

    public bool IsValidDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return false;
        }
        
        var lowered = description.ToLowerInvariant();
        var words = Regex.Split(lowered, @"\W+", RegexOptions.Compiled);

        return !_censoredWords.Any(censored => words.Contains(censored));
    }

    public async Task<bool> IsValidAuthor(int authorId)
    {
        return await _authorsRepository.GetById(authorId) != null;
    }
}