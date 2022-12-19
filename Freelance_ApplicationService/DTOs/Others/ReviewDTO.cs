using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.DTOs.Others
{
    public class ReviewDTO : BaseEntity
    {
        public int UserId { get; set; }
        public int ReviewedUserId { get; set; }
        public DateTime DateOfPosting { get; set; }
        public bool DeliveredOnBudget { get; set; }
        public bool DeliveredOnTime { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int TaskId { get; set; }
    }
}
