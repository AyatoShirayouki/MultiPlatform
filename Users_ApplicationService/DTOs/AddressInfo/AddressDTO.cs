using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_ApplicationService.DTOs.AddressInfo
{
    public class AddressDTO : BaseEntity
    {
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public int CityId { get; set; }
    }
}
