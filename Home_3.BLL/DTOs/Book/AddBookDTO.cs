namespace Home_3.BLL.DTOs.Book;

public class AddBookDTO
{
    public required string Name { get; set; }
    public int PublicationYear { get; set; }
    public int AuthorId { get; set; }
    public required string Description { get; set; }
}