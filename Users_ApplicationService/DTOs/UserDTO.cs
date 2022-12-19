using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_ApplicationService.DTOs
{
    public class UserDTO : Person
    {
        
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public string? LinkedInAccount { get; set; }
        public int AccountType { get; set; }
        public bool IsCompany { get; set; }
        public string? CompanyName { get; set; }
        public int AddressId { get; set; }
        public int PricingPlanId { get; set; }
    }
}
