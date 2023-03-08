using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.DTOs.Others
{
    public class CategoryDTO : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CategoryIcon { get; set; }
    }
}
