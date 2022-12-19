using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities.AddressInfo;

namespace Users_Data.Entities
{
    public class PricingPlanFeature : BaseEntity
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public int PricingPlanId { get; set; }

        [ForeignKey("PricingPlanId")]
        public PricingPlan? ParentPricingPlan { get; set; }
    }
}
