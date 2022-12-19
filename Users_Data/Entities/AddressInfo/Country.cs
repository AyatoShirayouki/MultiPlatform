using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_Data.Entities.AddressInfo
{
    public class Country : BaseEntity
    {
        [MaxLength(255)]
        [Required]
        public string? Name { get; set; }
        [MaxLength(2)]
        [Required]
        public string? Code { get; set; }
        [MaxLength(3)]
        [Required]
        public string? Language { get; set; }
    }
}
