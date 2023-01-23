using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using Library.Models;

namespace Library.CustomTagHelpers
{
  [HtmlTargetElement("td", Attributes = "i-role")]
  public class RoleUsersTH : TagHelper
  {
    private UserManager<ApplicationUser> userManager;
    private RoleManager<IdentityRole> roleManager;

    public RoleUsersTH(UserManager<ApplicationUser> usermgr, RoleManager<IdentityRole> rolemgr)
    {
      userManager = usermgr;
      roleManager = rolemgr;
    }

    [HtmlAttributeName("i-role")]
    public string Role { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
      List<string> names = new List<string>();
      IdentityRole role = await roleManager.FindByIdAsync(Role);
      if (role != null)
      {
        foreach (var user in userManager.Users)
        {
          if (user != null && await userManager.IsInRoleAsync(user, role.Name))
          names.Add(user.UserName);
        }
      }
      output.Content.SetContent(names.Count == 0 ? "No Users" : string.Join(", ", names));
    }
  }
}