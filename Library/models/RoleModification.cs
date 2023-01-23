using System.ComponentModel.DataAnnotations;
using Library.Models;

namespace Library.Models
{
  public class RoleModification
  {
    [Required]
    public string RoleNme { get; set; }
    public string RoleId { get; set; }

    #nullable enable
    public string[]? AddIds { get; set;}
    public string[]? DeleteIds { get; set; } 
    #nullable disable
  }
}