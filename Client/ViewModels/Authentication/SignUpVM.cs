using System.ComponentModel.DataAnnotations;
using Users_ApplicationService.DTOs.AddressInfo;

namespace Client.ViewModels.Authentication
{
    public class SignUpVM
    {
        [Required(ErrorMessage = "This field is required!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string? Gender { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public string? LinkedInAccount { get; set; }
        public bool IsCompany { get; set; }
        public string? CompanyName { get; set; }
        public string? AddressInfo { get; set; }

		[Required(ErrorMessage = "This field is required!")]
        public int AccountType { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public int AddressId { get; set; }

        public List<CountryDTO> Countries { get; set; }
        public List<RegionDTO> Regions { get; set; }
        public List<CityDTO> Cities { get; set; }
    }
}
