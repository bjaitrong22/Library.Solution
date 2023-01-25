using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using Library.Models;

namespace Library.Controllers
{
  public class CatalogsController: Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public CatalogsController(UserManager<ApplicationUser> userManager,LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      List<Catalog> model = _db.Catalogs.ToList();
      return View(model);
    }

    [Authorize]
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Catalog catalog)
    {
      if(!ModelState.IsValid)
      {
        return View(catalog);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        _db.Catalogs.Add(catalog);
        _db.SaveChanges();

        return RedirectToAction("Index");
      }
    }
  }

}