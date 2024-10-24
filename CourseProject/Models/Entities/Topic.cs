using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.Models.Entities;

public class Topic
{
    public Guid TopicId{ get; set; }
    [Column(TypeName = "nvarchar(20)")]
    public string Name { get; set; }
    public ICollection<Template> Templates { get; set; }
    
}