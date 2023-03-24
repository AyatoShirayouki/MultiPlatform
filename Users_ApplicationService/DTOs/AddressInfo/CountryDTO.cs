using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_ApplicationService.DTOs.AddressInfo
{
    public class CountryDTO : BaseEntity
    {
		public string? Name { get; set; }
		public string? Iso3 { get; set; }
		public string? NumericCode { get; set; }
		public string? Iso2 { get; set; }
		public string? PhoneCode { get; set; }
		public string? Capital { get; set; }
		public string? Currency { get; set; }
		public string? CurrencyName { get; set; }
		public string? CurrencySymbol { get; set; }
		public string? Tld { get; set; }
		public string? Native { get; set; }
		public string? Region { get; set; }
		public string? Subregion { get; set; }
		public string? Timezones { get; set; }
		public string? Translations { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		public string? Emoji { get; set; }
		public string? EmojiU { get; set; }
	}
}
