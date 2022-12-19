using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.DTOs.JobRelated
{
    public class JobApplicationDTO : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime DateOfAppying { get; set; }
        public int JobId { get; set; }
    }
}
