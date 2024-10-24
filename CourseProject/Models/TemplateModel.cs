using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.Models
{
    public class TemplateModel
    {
        public Guid TemplateId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public TopicModel Topic { get; set; }
        public List<TopicModel> Topics { get; set; } 
        public string ImageUrl { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<TagModel> Tags { get; set; } = new();
        
        public List<UserModel> Users { get; set; } = new ();
		public UserModel UserCreator { get; set; }
        public List<QuestionModel> Questions { get; set; } = new ();

        public string SearchTerm { get; set; }
		public List<SelectListItem> UsersUi { get; set; } = new ();
        public List<string> UsersId { get; set; } = new ();
        public List<SelectListItem> SelectedUsersUi { get; set; } = new (); 
	

    }
}
