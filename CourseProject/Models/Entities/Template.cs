using System;
using System.Collections.Generic;
using Azure;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CourseProject.Models.Entities
{
	public class Template
	{
		public Guid TemplateId { get; set; }

		[Column(TypeName = "nvarchar(50)")]
		public string Title { get; set; }

		[Column(TypeName = "nvarchar(1000)")]
		public string Description { get; set; }

		[Column(TypeName = "nvarchar(300)")]
		public string ImageUrl { get; set; }
		public bool IsPublic { get; set; }

		[Column(TypeName = "SMALLDATETIME")]
		public DateTime CreatedAt { get; set; }

		[Column(TypeName = "SMALLDATETIME")]
		public DateTime? UpdatedAt { get; set; }
		public string TemplateAuthorId {get;set ;}
		[ForeignKey("TemplateAuthorId")]
		public User templateAuthor { get; set; } 

		public Guid TopicId { get; set; }
		[ForeignKey("TopicId")]
		public Topic TopicName { get; set; } 
		
		public ICollection<User> UsersWithAccess { get; set; }
		public ICollection<CustomForm> Forms { get; set; }
		public ICollection<Question> Questions { get; set; }

		public ICollection<Comment> Comments { get; set; }
		public ICollection<Like> Likes { get; set; }
		public ICollection<Tag> Tags { get; set; }

	}
}
