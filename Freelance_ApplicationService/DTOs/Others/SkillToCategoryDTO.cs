using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.DTOs.Others
{
    public class SkillToCategoryDTO : BaseEntity
    {
        public int CategoryId { get; set; }
        public int SkillId { get; set; }
    }
}
