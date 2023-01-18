namespace Library.Models
{
  public class AuthorBooks
  {
    public int AuthorBooksId { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
  }  
}