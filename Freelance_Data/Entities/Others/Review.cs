using Base.Entities;
using Freelance_Data.Entities.TaskRelated;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Task = Freelance_Data.Entities.TaskRelated.Task;

namespace Freelance_Data.Entities.Others
{
    public class Review : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ReviewedUserId { get; set; }

        [Required]
        public DateTime DateOfPosting { get; set; }

        [Required]
        public bool DeliveredOnBudget { get; set; }

        [Required]
        public bool DeliveredOnTime { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? Comment { get; set; }

        [Required]
        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public Task? ParentTask { get; set; }
    }
}
