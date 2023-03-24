using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace Users_Data.Entities.AddressInfo
{
    public class Country : BaseEntity
    {
        [MaxLength(100)]
        [Required]
        public string? name { get; set; }

        [MaxLength(3)]
        public string? iso3 { get; set; }

		[MaxLength(3)]
		public string? numeric_code { get; set; }

		[MaxLength(3)]
        public string? iso2 { get; set; }

		[MaxLength(255)]
		public string? phonecode { get; set; }

		[MaxLength(255)]
        public string? capital { get; set; }

		[MaxLength(255)]
		public string? currency { get; set; }

		[MaxLength(255)]
		public string? currency_name { get; set; }

		[MaxLength(255)]
		public string? currency_symbol { get; set; }

		[MaxLength(255)]
		public string? tld { get; set; }

		[MaxLength(255)]
		public string? native { get; set; }

		[MaxLength(255)]
		public string? region { get; set; }

		[MaxLength(255)]
		public string? subregion { get; set; }

		[MaxLength(2000)]
		public string? timezones { get; set; }
		[MaxLength(2000)]
		public string? translations { get; set; }
        public decimal latitude { get; set; }
		public decimal longitude { get; set; }

		[MaxLength(191)]
		public string? emoji { get; set; }

		[MaxLength(191)]
		public string? emojiU { get; set; }
	}
}
