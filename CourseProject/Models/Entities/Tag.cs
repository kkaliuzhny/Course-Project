using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models.Entities
{
	public class Tag
	{
		public Guid TagId { get; set; }

		[Column(TypeName = "nvarchar(40)")]
		public string Tag_Name { get; set; }

		public ICollection<Template> Templates { get; set; }
	}
}
