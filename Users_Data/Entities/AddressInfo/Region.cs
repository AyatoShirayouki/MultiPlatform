using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Users_Data.Entities.AddressInfo
{
    public class Region : BaseEntity
    {
        [MaxLength(255)]
        [Required]

        public string? name { get; set; }
        [Required]
        public int country_id { get; set; }

		[Required]
		[MaxLength(2)]
		public string? country_code { get; set; }

		[MaxLength(191)]
		public string? type { get; set; }
        public decimal latitude { get; set; }
		public decimal longitude { get; set; }

		[ForeignKey("country_id")]
        public Country? ParentCountry { get; set; }
    }
}
