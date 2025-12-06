using Home_3.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace Home_3.DAL;

public class HomeContext : DbContext
{
    public HomeContext(DbContextOptions<HomeContext> options) : base(options)
    {
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
}