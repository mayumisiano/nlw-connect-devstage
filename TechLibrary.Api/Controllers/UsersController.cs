using Microsoft.AspNetCore.Mvc;

namespace TechLibrary.Api.Controllers;

public class UsersController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}