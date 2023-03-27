using System.ComponentModel.DataAnnotations;
using Users_ApplicationService.DTOs.AddressInfo;

namespace Client.ViewModels.Authentication
{
    public class SignUpVM
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public string? LinkedInAccount { get; set; }
        public bool IsCompany { get; set; }
        public string? CompanyName { get; set; }
        public string? AddressInfo { get; set; }
        public int AccountType { get; set; }
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public int CityId { get; set; } 
    }
}
