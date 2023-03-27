using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_ApplicationService.DTOs.AddressInfo
{
    public class CityDTO : BaseEntity
    {
		public string? Name { get; set; }
		public int RegionId { get; set; }
		public string? RegionCode { get; set; }
		public int CountryId { get; set; }
		public string? CountryCode { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		public string? WikiDataId { get; set; }
	}
}
