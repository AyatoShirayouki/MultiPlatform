using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_ApplicationService.DTOs
{
    public class PricingPlanDTO : BaseEntity
    {
        public decimal Price { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
