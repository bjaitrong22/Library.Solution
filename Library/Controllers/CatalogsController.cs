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

    public ActionResult Details(int id)
    {
        Catalog thisCatalog = _db.Catalogs
                              .Include(cat => cat.Books)
                              .ThenInclude(book => book.JoinEntities)
                              .FirstOrDefault(catalog => catalog.CatalogId == id);

        return View(thisCatalog);
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      Catalog thisCatalog = _db.Catalogs.FirstOrDefault(catalog => catalog.CatalogId == id);

      return View(thisCatalog);
    }

    [HttpPost]
    public async Task <ActionResult> Edit(Catalog catalog)
    {
      if (!ModelState.IsValid)
      {
        return View(catalog);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        _db.Catalogs.Update(catalog);
        _db.SaveChanges();

        return RedirectToAction("Index");
      }
    }

    [Authorize]
    public ActionResult Delete(int id)
    {
      Catalog thisCatalog = _db.Catalogs.FirstOrDefault(catalog => catalog.CatalogId == id);

      return View(thisCatalog);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      
      Catalog thisCatalog = _db.Catalogs.FirstOrDefault(catalog => catalog.CatalogId == id);
      _db.Catalogs.Remove(thisCatalog);
      _db.SaveChanges();

      return RedirectToAction("Index");  
    }
  }
}