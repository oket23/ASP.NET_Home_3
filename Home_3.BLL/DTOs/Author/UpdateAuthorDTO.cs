namespace Home_3.BLL.DTOs.Author;

public class UpdateAuthorDTO
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthdayDate { get; set; }
}