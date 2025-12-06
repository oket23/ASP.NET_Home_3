using Home_3.BLL.Models;

namespace Home_3.BLL.interfaces.Services;

public interface IValidationService
{
    bool IsValidDescription(string description);
    Task<bool> IsValidAuthor(int authorId);
}