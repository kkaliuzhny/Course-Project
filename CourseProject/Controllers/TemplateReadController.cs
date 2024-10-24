using System;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Data;
using CourseProject.Models;
using CourseProject.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Controllers;

public class TemplateReadController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly CustomFormsDbContext _context;

    public TemplateReadController(UserManager<User> userManager, CustomFormsDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> AddComment(Guid template, string commentText)
    {
       
        var user = await _userManager.GetUserAsync(User);
        var comment = new Comment
        {
            
            CommentText = commentText,
            UserId = user.Id,
            TemplateId = template
        };
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return Ok(new { message = "success" });
    }
    [HttpPost]
    public async Task<IActionResult> AddLike(Guid templateId)
    {
        var user = await _userManager.GetUserAsync(User);
        var existingLike = _context.Likes.FirstOrDefault(l => l.TemplateId == templateId && l.UserId == user.Id);
        if (existingLike is null)
        {
            var newLike = new Like
            {
                UserId = user.Id,
                TemplateId = templateId
            };

            _context.Likes.Add(newLike);
            await _context.SaveChangesAsync();
            return Ok(new { message = "success" });
        }

        return BadRequest(new { message = "exist" });
    }
    
    [HttpGet]
    public async Task<IActionResult> ShowCreatedTemplate(Template template)
    { 
        var id = template.TemplateId;
        
        int likesAmount = _context.Likes.Count(l => l.TemplateId == id);
        ViewBag.LikesAmount = likesAmount;
        var template1 = _context.Templates
            .Include(t => t.Questions) 
            .Include(c=>c.Comments)
            .FirstOrDefault(t => t.TemplateId == id);
        
        return View(template1);
    }
}