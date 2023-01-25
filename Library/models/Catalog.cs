using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Library.Models
{
  public class Catalog
  {
    public int CatalogId { get; set; }
    [Required(ErrorMessage = "The Library Name for the catalog can't be empty!")]
    public string LibraryName { get; set; }
    public List<Book> Books { get; set; }
  }
}