using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.DTOs.TaskRelated
{
    public class TaskDTO : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime DateOfPosting { get; set; }
        public string? ProjectName { get; set; }
        public string? Location { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? TaskType { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
    }
}
