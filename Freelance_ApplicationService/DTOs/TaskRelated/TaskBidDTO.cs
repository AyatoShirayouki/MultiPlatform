using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.DTOs.TaskRelated
{
    public class TaskBidDTO : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime DateOfBidding { get; set; }
        public decimal MinimalRate { get; set; }
        public int DeliveryTimeCount { get; set; }
        public string? DeliveryTimeType { get; set; }
        public int Status { get; set; }
        public int TaskId { get; set; }
    }
}
