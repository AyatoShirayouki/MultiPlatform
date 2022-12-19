using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_ApplicationService.DTOs
{
    public class PricingPlanFeatureDTO : BaseEntity
    {
        public string? Name { get; set; }
        public int PricingPlanId { get; set; }
    }
}
