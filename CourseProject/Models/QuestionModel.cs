using System;
using CourseProject.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.Models
{
    public class QuestionModel
    {
        public Guid QuestionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool IsShown { get; set; }
        public int Order { get; set; }

       
    }
}
