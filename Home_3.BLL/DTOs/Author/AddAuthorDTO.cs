namespace Home_3.BLL.DTOs.Author;

public class AddAuthorDTO
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime BirthdayDate { get; set; }
}