using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CourseProject.Models.Entities
{
	public class Like
	{
		public Guid LikeId { get; set; }
		
		public string UserId { get; set; }
		[ForeignKey("UserId")]
		public User user { get; set; }

		public Guid TemplateId { get; set; }
		[ForeignKey("TemplateId")]
		public Template template { get; set; }
	}
}
