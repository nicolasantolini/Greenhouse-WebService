using Microsoft.AspNetCore.Mvc;

namespace DatabaseWebAPI.Controllers;

public class DatabaseWebAPI : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}