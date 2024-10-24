using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.Design;

namespace CourseProject.Models.Entities
{
	public class User : IdentityUser
	{
		[Column(TypeName = "nvarchar(20)")]
		public string Theme { get; set; }

		[Column(TypeName = "nvarchar(15)")]
		public string Language { get; set; }

		//[Column(TypeName = "nvarchar(10)")]
  //      public string Status { get; set; } = "active";
		

        public ICollection<CustomForm> Forms { get; set; }
		public ICollection<Template> CreatedTemplates { get; set; }
		public ICollection<Template> AccesibleTemplates { get; set; }
		public ICollection<Like> Likes { get; set; }
		public ICollection<Comment> Comments { get; set; }


	}
}
