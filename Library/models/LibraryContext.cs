using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Library.Models
{
  public class LibraryContext: IdentityDbContext<ApplicationUsers>
  {
    public DbSet<Catalog> Catalog { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<AuthorBooks> AuthorBooks { get; set; }

    public LibraryContext(DbContextOptions contextOptions) : base(options) {}
  }
}