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
    public class User : Person
    {
        [MaxLength(6)]
        public string? Gender { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }
        [MaxLength(120)]
        public string? LinkedInAccount { get; set; }
        public bool IsCompany { get; set; }
        public string? CompanyName { get; set; }

        [Required]
        public int AccountType { get; set; }

        [Required]
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address? ParentAddress { get; set; }

        public int PricingPlanId { get; set; }

        [ForeignKey("PricingPlanId")]
        public PricingPlan? ParentPricingPlan { get; set; }
    }
}
