using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admins_Data.Entities
{
    public class AdminFile : BaseFile
    {
        public int AdminId { get; set; }
        public bool IsProfilePhoto { get; set; }

        [ForeignKey("AdminId")]
        public Admin? ParentAdmin { get; set; }
    }
}
