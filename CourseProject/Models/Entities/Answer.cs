using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models.Entities
{
	public class Answer
	{ 
		public Guid AnswerId { get; set; }
		public string AnswerText { get; set; }

		public Guid QuestionId { get; set; }

		[ForeignKey("QuestionId")]
		public Question question { get; set; }

		public Guid CustomFormId { get; set; }
		[ForeignKey("CustomFormId")]
		public CustomForm customForm { get; set; }

	}
}
