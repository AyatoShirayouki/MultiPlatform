using Base.Entities;
using Freelance_Data.Entities.TaskRelated;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Freelance_Data.Entities.TaskRelated.Task;

namespace Freelance_Data.Entities.Bookmarks
{
    public class BookmarkedTask : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public Task? ParentTask { get; set; }
    }
}
