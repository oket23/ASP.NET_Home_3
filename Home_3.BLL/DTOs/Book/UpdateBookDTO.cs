namespace Home_3.BLL.DTOs.Book;

public class UpdateBookDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? PublicationYear { get; set; }
    public int? AuthorId { get; set; }
    public string? Description { get; set; }
}
