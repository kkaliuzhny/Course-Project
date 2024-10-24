using System;
using System.Collections.Generic;
using CourseProject.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace CourseProject.Data
{
    public class CustomFormsDbContext : IdentityDbContext<User>
    {
        public CustomFormsDbContext(DbContextOptions<CustomFormsDbContext> options) : base(options)
        {
        }

       
        public DbSet<Template> Templates { get; set; }
        public DbSet<CustomForm> CustomForms { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Topic> Topics { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Template>().HasMany(q => q.Questions)
                .WithOne(t => t.Template)
                .HasForeignKey(k => k.TemplateId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>().HasMany(t => t.AccesibleTemplates)
                .WithMany(u => u.UsersWithAccess)
                .UsingEntity(j => j.ToTable("UserAccessControl"));


            modelBuilder.Entity<Tag>().HasMany(x => x.Templates)
                .WithMany(y => y.Tags)
                .UsingEntity(j => j.ToTable("TemplateTags"));

            modelBuilder.Entity<User>().HasMany(t => t.CreatedTemplates)
                .WithOne(u => u.templateAuthor)
                .HasForeignKey(k => k.TemplateAuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Topic>().HasMany(t => t.Templates)
                .WithOne(t => t.TopicName)
                .HasForeignKey(t => t.TopicId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Question>()
                .Property(q => q.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Template>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<User>()
                .Property(t => t.Theme)
                .HasDefaultValue("light");

            modelBuilder.Entity<User>()
                .Property(l => l.Language)
                .HasDefaultValue("En");
            
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.user)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Restrict deletion for User


            modelBuilder.Entity<Comment>()
                .HasOne(c => c.template)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TemplateId)
                .OnDelete(DeleteBehavior.NoAction); // Allow cascading delete for Template

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.NoAction); // Указываем DeleteBehavior.Restrict

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.customForm)
                .WithMany(cf => cf.Answers)
                .HasForeignKey(a => a.CustomFormId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.UserName, u.Email })
                .IsUnique()
                .HasDatabaseName("IX_Unique_Name_Email");
            
            modelBuilder.Entity<Topic>().HasData(new List<Topic>()
            {
                new Topic { TopicId = Guid.NewGuid(), Name = "Education" },
                new Topic { TopicId = Guid.NewGuid(), Name = "Quiz" },
                new Topic { TopicId = Guid.NewGuid(), Name = "Other" }
            });
            
        }
    }
}