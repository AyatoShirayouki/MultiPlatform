using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Data.Entities.JobRelated
{
    public class JobApplication : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime DateOfAppying { get; set; }

        [Required]
        public int JobId { get; set; }

        [ForeignKey("JobId")]
        public Job? ParentJob { get; set; }
    }
}
