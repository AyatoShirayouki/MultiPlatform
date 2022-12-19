using Freelance_Data.Entities.Bookmarks;
using Freelance_Data.Entities.JobRelated;
using Freelance_Data.Entities.Others;
using Freelance_Data.Entities.TaskRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Global;
using Task = Freelance_Data.Entities.TaskRelated.Task;

namespace Freelance_Data.Context
{
    public class FreelanceDbContext : DbContext
    {
        public DbSet<BookmarkedJob> BookmarkedJobs { get; set; }
        public DbSet<BookmarkedTask> BookmarkedTasks { get; set; }
        public DbSet<BookmarkedUser> BookmarkedUsers { get; set; }

        public DbSet<FileToJob> FilesToJobs { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<SkillToJob> SkillsToJobs { get; set; }
        public DbSet<TagToJob> TagsToJobs { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<FreelanceFile> FreelanceFiles { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillToCategory> SkillsToCategories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<FileToTask> FilesToTasks { get; set; }
        public DbSet<SkillToTask> SkillsToTasks { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskBid> TaskBids { get; set; }

        public FreelanceDbContext()
        {
            BookmarkedJobs = this.Set<BookmarkedJob>();
            BookmarkedTasks = this.Set<BookmarkedTask>();
            BookmarkedUsers = this.Set<BookmarkedUser>();

            FilesToJobs = this.Set<FileToJob>();
            Jobs = this.Set<Job>();
            JobApplications = this.Set<JobApplication>();
            JobTypes = this.Set<JobType>();
            SkillsToJobs = this.Set<SkillToJob>();
            TagsToJobs = this.Set<TagToJob>();

            Categories = this.Set<Category>();
            FreelanceFiles = this.Set<FreelanceFile>();
            Notes = this.Set<Note>();
            Reviews = this.Set<Review>();
            Skills = this.Set<Skill>();
            SkillsToCategories = this.Set<SkillToCategory>();
            Tags = this.Set<Tag>();

            FilesToTasks = this.Set<FileToTask>();
            SkillsToTasks = this.Set<SkillToTask>();
            Tasks = this.Set<Task>();
            TaskBids = this.Set<TaskBid>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GlobalVariables.Freelance_DB_CN);
        }
    }
}
