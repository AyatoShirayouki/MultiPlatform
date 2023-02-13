using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_Data.Entities.AddressInfo
{
    public class Address : BaseEntity
    {
        [Required]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country? ParentCountry { get; set; }

        [Required]
        public int RegionId { get; set; }

        [ForeignKey("RegionId")]
        public Region? ParentRegion { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public string? AddressInfo { get; set; }

        [ForeignKey("CityId")]
        public City? ParentCity { get; set; }
    }
}
