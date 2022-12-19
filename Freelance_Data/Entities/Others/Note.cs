using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Data.Entities.Others
{
    public class Note : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Content { get; set; }

        [Required]
        public int Priority { get; set; }
    }
}
