using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_Data.Entities.Education
{
    public class Degree : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
    }
}
