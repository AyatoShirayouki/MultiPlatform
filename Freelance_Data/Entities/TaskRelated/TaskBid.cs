using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Data.Entities.TaskRelated
{
    public class TaskBid : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime DateOfBidding { get; set; }

        [Required]
        public decimal MinimalRate { get; set; }

        [Required]
        public int DeliveryTimeCount { get; set; }

        [Required]
        public string? DeliveryTimeType { get; set; }

        public int Status { get; set; }

        [Required]
        public int TaskId { get; set; }

        [ForeignKey("TaskId")]
        public Task? ParentTask { get; set; }
    }
}
