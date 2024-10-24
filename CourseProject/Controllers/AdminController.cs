using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Controllers;

public class AdminController : Controller
{
    [HttpGet]
    public IActionResult GetAdminPage()
    {
        return View();
    }
}