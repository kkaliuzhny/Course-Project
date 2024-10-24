﻿using CourseProject.Models.Entities;

namespace CourseProject.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }   
        public DateTimeOffset? Status{ get; set; }
    }
}
