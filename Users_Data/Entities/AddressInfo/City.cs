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
    public class City : BaseEntity
    {
        [MaxLength(255)]
        [Required]
        public string? Name { get; set; }
        [Required]
        public int RegionId { get; set; }

        [ForeignKey("RegionId")]
        public Region? ParentRegion { get; set; }
    }
}
