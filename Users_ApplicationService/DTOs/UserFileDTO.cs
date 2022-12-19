using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_ApplicationService.DTOs
{
    public class UserFileDTO : BaseFile
    {
        public int UserId { get; set; }
        public bool IsProfilePhoto { get; set; }
        public bool IsCoverPhoto { get; set; }
        public bool IsCV { get; set; }
    }
}
