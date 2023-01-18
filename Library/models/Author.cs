using System.Collections.Generic;

namespace Library.Models
{
  public class Author
  {
    public int AuthorId { get; set; }
    public int AuthorName { get; set; }
    public List<AuthorBook> JoinEntities { get; set; }
  }
}