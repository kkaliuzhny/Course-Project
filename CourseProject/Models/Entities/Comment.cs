using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.Models.Entities
{
	public class Comment
    	{
    		public Guid CommentId { get; set; }
		public string CommentText { get; set; }

		public string UserId { get; set; }
		[ForeignKey("UserId")]
		public User user { get; set; }

		public Guid TemplateId { get; set; }
		[ForeignKey("TemplateId")]

		public Template template { get; set; }

	}
}
