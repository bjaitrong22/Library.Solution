using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Library.Models;

namespace Library.Controllers
{
  public class CatalogsController: Controller
  {
    private readonly LibraryContext _db;
    public CatalogsController(LibraryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Catalog> model = _db.Catalogs.ToList();
      return View(model);
    }


  }

}