using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Library.Models;

namespace Library.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }  
  }  
}