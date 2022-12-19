using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Data.Entities.JobRelated
{
    public class JobType : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
    }
}
