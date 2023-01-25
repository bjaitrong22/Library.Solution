using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;


//using Microsoft.AspNetCore.Authorization;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Mvc;
//using System.ComponentModel.DataAnnotations;



namespace Library.CustomTagHelpers
{
    [HtmlTargetElement("td", Attributes = "i-role")]
    public class RoleUsersTH : TagHelper
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public RoleUsersTH(UserManager<ApplicationUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            userManager = userMgr;
            roleManager = roleMgr;
        }
 
        [HtmlAttributeName("i-role")]
        public string Role { get; set; }
 
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
          List<string> names = new List<string>();
          IdentityRole role = await roleManager.FindByIdAsync(Role);
          if (role != null)
          {
            foreach (var user in userManager.Users.ToList())
            {
              if (user != null && await userManager.IsInRoleAsync(user, role.Name))
                names.Add(user.UserName);
            }
          }
          output.Content.SetContent(names.Count == 0 ? "No Users" : string.Join(", ", names));
        }
    }
}  