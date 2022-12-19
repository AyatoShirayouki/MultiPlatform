using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_Data.Entities
{
    public class PricingPlan : BaseEntity
    {
        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(150)]
        public string? Description { get; set; }
    }
}
