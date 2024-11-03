using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CourseProject.Data;
using CourseProject.Models;
using CourseProject.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CourseProject.Controllers
{
    public class TemplateController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly CustomFormsDbContext _context;
        private readonly Cloudinary _cloudinary;
        public TemplateController(UserManager<User> userManager, CustomFormsDbContext context, Cloudinary cloudinary)
        {
            _userManager = userManager;
            _context = context;
            _cloudinary = cloudinary;
        }

        [HttpGet]
        public IActionResult ShowTemplate()
        {
            var topics = _context.Topics.ToList();
            bool isPublic = true;
         
            var questionTypes = Enum.GetValues(typeof(Question.QuestionType))
                .Cast<Question.QuestionType>()
                .Select(q => new SelectListItem
                {
                    Value = q.ToString(),
                    Text = q.ToString()
                }).ToList();
            ViewBag.QuestionTypes = questionTypes;
            
            var topicModels = topics.Select(t => new TopicModel 
            {
                TopicId = t.TopicId,
                Name = t.Name
            }).ToList();
            var templateModel = new TemplateModel()
            {
                Topics = topicModels,
                IsPublic = isPublic
            };
            return View(templateModel);

        }

        [HttpPost]
        public JsonResult CheckUserExistence(string userName)
        {
            int startIndex = userName.IndexOf("(");
            int secondIndex = userName.IndexOf(")");
            userName = userName.Remove(startIndex, secondIndex - startIndex + 1);
            var userExists = _context.Users.Any(u => u.UserName == userName);
            return Json(userExists);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AutoComplete(string prefix)
        {
            var users = _context.Users
                .Where(u => u.UserName.StartsWith(prefix) || u.Email.StartsWith(prefix))
                .Select(u => new
                {
                    label = $"{u.UserName}({u.Email})",
                    value = $"{u.UserName}({u.Email})"
                }).ToList();

            return Json(users);
        }

        public JsonResult TagAutoComplete(string prefix)
        {
            var tags = _context.Tags
                .Where(t => t.Tag_Name.StartsWith(prefix))
                .Select(t => new { label = t.Tag_Name, value = t.TagId })
                .ToList();

            return Json(tags);
        }
     

        [HttpPost]
        public async Task<IActionResult> CreateTemplate(TemplateModel templateModel, string selectedUsers)
        {
            if (!templateModel.IsPublic)
            {
                var usersWithAccess = JsonConvert.DeserializeObject<List<string>>(selectedUsers);
            }
            
            var user = await _userManager.GetUserAsync(HttpContext.User);
            bool isPublic = templateModel.IsPublic;
            var topicId = templateModel.Topic.TopicId;
            var questions = templateModel.Questions;
            ICollection<Question> questionsToSend =  questions.Select(q=> new Question
            {
                Title = q.Title,
                Description = q.Description,
                IsShown = q.IsShown,
                Order = q.Order,
                Type = Enum.TryParse<Question.QuestionType>(q.Type, true, out var questionType) ?
                                                        questionType: Question.QuestionType.SingleLine
            }).ToList();
            
            
            Template template = new Template
            {
                Title = templateModel.Title,
                Description = templateModel.Description,
                TopicId = templateModel.Topic.TopicId,
                ImageUrl = templateModel.ImageUrl,
                TemplateAuthorId = user.Id,
                IsPublic = isPublic,
                Questions = questionsToSend,
                Forms = new List<CustomForm>(),
                Comments = new List<Comment>(), 
                Likes = new List<Like>(), 
                Tags = new List<Tag>() 
            };
            
            _context.Templates.Add(template);
            await _context.SaveChangesAsync();
            return View("_LoginPartial");
        }



   
        //[HttpPost]
        //public async Task<IActionResult> CreateTemplate(TemplateModel templateModel)
        //{


        //		
        //		bool isPublic = false;
        //		if (templateModel.UsersId.Length == _userManager.Users.ToList().Count)
        //		{
        //			isPublic = true;
        //		}
        //		Template template = new Template()
        //		{
        //			TemplateId = templateModel.TemplateId,
        //			Title = templateModel.Title,
        //			Description = templateModel.Description,
        //			Topic = templateModel.Topic,
        //			ImageUrl = templateModel.ImageUrl,
        //			TemplateAuthorId = user.Id,
        //			IsPublic = isPublic,
        //			UsersWithAccess = new List<User>(),
        //			Forms = new List<CustomForm>(), // Инициализируем Forms
        //			Questions = new List<Question>(), // Инициализируем Questions
        //			Comments = new List<Comment>(), // Инициализируем Comments
        //			Likes = new List<Like>(), // Инициализируем Likes
        //			Tags = new List<Tag>() // Инициализируем Tags


        //		};
        //		if (templateModel.UsersId != null)
        //		{
        //			foreach (var selectedUserId in templateModel.UsersId)
        //			{
        //				var selectedUser = await _userManager.FindByIdAsync(selectedUserId);
        //				if (selectedUser != null)
        //				{
        //					template.UsersWithAccess.Add(selectedUser);
        //				}
        //			}
        //		}
        //		_context.Templates.Add(template);
        //		await _context.SaveChangesAsync();
        //		return View("_LoginPartial");


        //}
    }
}