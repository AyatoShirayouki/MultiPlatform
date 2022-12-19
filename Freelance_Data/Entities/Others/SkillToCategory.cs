using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Data.Entities.Others
{
    public class SkillToCategory : BaseEntity
    {
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? ParentCategory { get; set; }

        [Required]
        public int SkillId { get; set; }

        [ForeignKey("SkillId")]
        public Skill? ParentSkill { get; set; }
    }
}
