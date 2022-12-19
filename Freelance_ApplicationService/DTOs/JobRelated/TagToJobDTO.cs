using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.DTOs.JobRelated
{
    public class TagToJobDTO : BaseEntity
    {
        public int TagId { get; set; }
        public int JobId { get; set; }
    }
}
