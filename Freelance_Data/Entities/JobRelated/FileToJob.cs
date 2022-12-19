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
    public class FileToJob : BaseEntity
    {
        [Required]
        public int FileId { get; set; }

        [ForeignKey("FileId")]
        public FreelanceFile? ParentFreelanceFile { get; set; }

        [Required]
        public int JobId { get; set; }

        [ForeignKey("JobId")]
        public Job? ParentJob { get; set; }
    }
}
