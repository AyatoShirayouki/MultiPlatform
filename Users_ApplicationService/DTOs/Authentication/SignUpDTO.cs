using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_ApplicationService.DTOs.AddressInfo;

namespace Users_ApplicationService.DTOs.Authentication
{
    public class SignUpDTO
    {
        public UserDTO? User { get; set; }
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public int CityId { get; set; }
        public string? AddressInfo { get; set; }
    }
}
