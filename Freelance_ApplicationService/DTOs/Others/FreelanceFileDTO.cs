using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.DTOs.Others
{
    public class FreelanceFileDTO : BaseFile
    {
        public byte[]? File { get; set; }
    }
}
