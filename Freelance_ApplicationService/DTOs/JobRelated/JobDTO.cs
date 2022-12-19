using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.DTOs.JobRelated
{
    public class JobDTO : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime DateOfPosting { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int JobTypeId { get; set; }
    }
}
