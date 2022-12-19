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
        public string? Code { get; set; }
        public string? Language { get; set; }
    }
}
