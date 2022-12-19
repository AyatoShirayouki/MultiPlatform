using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_Data.Entities.AddressInfo
{
    public class Region : BaseEntity
    {
        [MaxLength(255)]
        [Required]
        public string? Name { get; set; }
        [Required]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country? ParentCountry { get; set; }
    }
}
