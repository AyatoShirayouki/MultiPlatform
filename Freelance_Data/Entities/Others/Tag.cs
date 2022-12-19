using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Data.Entities.Others
{
    public class Tag : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
    }
}
