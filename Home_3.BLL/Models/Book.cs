namespace Home_3.BLL.Models;

public class Book
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int PublicationYear { get; set; }
    public int AuthorId { get; set; }
    public Author? Author { get; set; }
    public required string Description { get; set; }
}