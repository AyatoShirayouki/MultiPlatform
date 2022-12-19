using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Freelance_Data.Entities.Others;

namespace Freelance_Data.Entities.JobRelated
{
    public class SkillToJob : BaseEntity
    {
        [Required]
        public int SkillId { get; set; }

        [ForeignKey("SkillId")]
        public Skill? ParentSkill { get; set; }

        [Required]
        public int JobId { get; set; }

        [ForeignKey("JobId")]
        public Job? ParentJob { get; set; }
    }
}
