using System.Linq;
using System.Threading.Tasks;
using CourseProject.Data;
using CourseProject.Models;
using CourseProject.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Controllers;

public class TemplateManagerController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly CustomFormsDbContext _context;

    public TemplateManagerController(UserManager<User> userManager, CustomFormsDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> ManageTemplates()
    {
        var userCurrent = await _userManager.GetUserAsync(User);
        var templates = _context.Templates.Where(t => t.templateAuthor == userCurrent).ToList();
        return View(templates);
    }

    [HttpGet]
    public async Task<IActionResult> ShowTemplate(Template template)
    {
        return Content("This feature will be implemented soon ;)");
        //return RedirectToAction("ShowCreatedTemplate","TemplateRead",template);
    }
   

    
    [HttpGet]
    public async Task<IActionResult> GetTemplate(string title)
    {
        Template template = await _context.Templates.FirstOrDefaultAsync(t=>t.Title == title);
        return View(template);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetQuestions(string title)
    {
        return Content("This feature will be implemented soon ;)");
    }
    
   
    
    public async Task<IActionResult> GetTemplateSettings()
    {
        return RedirectToAction("ShowTemplate","Template" );
    }
    private async Task<List<Template>> GetTemplatesToProcess(string[] selectedItems)
    {
        return await _context.Templates.Where(t => selectedItems.Contains(t.Title)).ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Edit()
    {
        return Content("This feature will be implemented soon ;)");
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        return RedirectToAction("ShowTemplate", "Template");
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(string[] selectedItems)
    {
        var templates = await  GetTemplatesToProcess(selectedItems);
        _context.Templates.RemoveRange(templates);
        await _context.SaveChangesAsync();
        return RedirectToAction("ManageTemplates");
    }
    
    
}