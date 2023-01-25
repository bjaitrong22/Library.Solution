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
  public class BooksController: Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public BooksController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {  
      List<Book> books = _db.Books
        .Include(book => book.Catalog)
        .ToList();

      return View(books);
    }

    [Authorize]
    public ActionResult Create()
    {
      ViewBag.CatalogId = new SelectList(_db.Catalogs, "CatalogId", "LibraryName");
      return View();

    }

    [HttpPost]
    public async Task<ActionResult> Create(Book book, int CatalogId)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.CatalogId = new SelectList(_db.Catalogs, "CatalogId", "LibraryName");
        return View(book);
      }
      else
      {
        string userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        book.User = currentUser;
        _db.Books.Add(book);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }
  }
}