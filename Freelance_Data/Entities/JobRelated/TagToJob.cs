using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Freelance_Data.Entities.Others;

namespace Freelance_Data.Entities.JobRelated
{
    public class TagToJob : BaseEntity
    {
        [Required]
        public int TagId { get; set; }

        [ForeignKey("TagId")]
        public Tag? ParentTag { get; set; }

        [Required]
        public int JobId { get; set; }

        [ForeignKey("JobId")]
        public Job? ParentJob { get; set; }
    }
}
