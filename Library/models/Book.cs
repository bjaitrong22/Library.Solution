using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
  public class Book 
  {
    public int BookId { get; set; }
    [Required(ErrorMessage = "The book Title can't be empty!")]
    public string Title { get; set; }
    [Range(1,int.MaxValue, ErrorMessage = "You must add your book to the catalog. Please create a catalog.")]
    public int CatalogId { get; set; }
    public Catalog Catalog { get; set; }
    public List<AuthorBook> JoinEntities { get; set; }
    public ApplicationUser User { get; set;}
  }
}