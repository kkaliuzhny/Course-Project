using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models.Entities
{
    
    public class Question
    {
        public enum QuestionType
        {
            SingleLine,
            MultiLine,
            PositiveInteger,
            Checkbox
        }
        public Guid QuestionId { get; set; }

        [Column(TypeName = "nvarchar(30)")] public string Title { get; set; }

        [Column(TypeName = "nvarchar(50)")] public string Description { get; set; }

        [Column(TypeName = "nvarchar(50)")] public QuestionType Type { get; set; }

        public bool IsShown { get; set; }
        public int Order { get; set; }

        public Guid TemplateId { get; set; }
        public Template Template { get; set; }

        public ICollection<Answer> Answers { get; set; }
        
    }
}