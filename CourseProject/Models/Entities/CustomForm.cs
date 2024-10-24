using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CourseProject.Models.Entities
{
	public class CustomForm
	{
		public Guid CustomFormId { get; set; }

		[Column(TypeName = "SMALLDATETIME")] public DateTime SubmissionDate { get; set; }

		public string UserId {  get; set; }
		[ForeignKey("UserId")]
		public User user { get; set; }

		public Guid TemplateId { get; set; }
		[ForeignKey("TemplateId")]
		public Template baseTemplate { get; set; }

		public ICollection<Answer> Answers { get; set; }
		
	}
}
