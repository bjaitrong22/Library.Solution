using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Library.Models;

namespace Library.Models
{
  public class RoleEdit
  {
    public IdentityRole Role { get; set; }
    public IEnumerable<ApplicationUser> Members { get; set; }
    public IEnumerable<ApplicationUser> NonMembers { get; set; }
  }
}