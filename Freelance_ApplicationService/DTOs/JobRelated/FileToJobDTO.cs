using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.DTOs.JobRelated
{
    public class FileToJobDTO : BaseEntity
    {
        public int FileId { get; set; }
        public int JobId { get; set; }
    }
}
