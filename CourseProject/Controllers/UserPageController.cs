using CourseProject.Data;
using CourseProject.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Controllers;

public class UserPageController : Controller
{
    [HttpGet]
    public IActionResult ShowPage()
    {
        return View();
    }
    
}