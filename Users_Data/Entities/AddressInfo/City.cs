using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Users_Data.Entities.AddressInfo
{
    public class City : BaseEntity
    {
        [MaxLength(255)]
        [Required]
        public string? name { get; set; }

        [Required]
        public int region_id { get; set; }

		[MaxLength(2)]
		[Required]
		public int region_code { get; set; }

		[Required]
		public int country_id { get; set; }

		[MaxLength(2)]
        [Required]
        public string? country_code { get; set; }
	
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }

		[MaxLength(255)]
        public string? wikiDataId { get; set; }

        [ForeignKey("region_id")]
        public Region? ParentRegion { get; set; }

		[ForeignKey("country_id")]
		public Country? ParentCountry { get; set; }
	}
}
