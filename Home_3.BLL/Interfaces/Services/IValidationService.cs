namespace Home_3.BLL.interfaces.Services;

public interface IValidationService
{
    bool IsValidDescription(string description);
    bool IsValidAuthor(int authorId);
}