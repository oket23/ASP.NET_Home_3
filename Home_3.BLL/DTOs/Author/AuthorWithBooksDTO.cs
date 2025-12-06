namespace Home_3.BLL.DTOs.Author;

public class AuthorWithBooksDTO
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime BirthdayDate { get; set; }
    public List<Models.Book> Books { get; set; } = new List<Models.Book>();
}