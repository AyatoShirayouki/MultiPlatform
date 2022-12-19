using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admins_ApplicationService.DTOs
{
    public class AdminFileDTO : BaseFile
    {
        public int AdminId { get; set; }
        public bool IsProfilePhoto { get; set; }
    }
}
