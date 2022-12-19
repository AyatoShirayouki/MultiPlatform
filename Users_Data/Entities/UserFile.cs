using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users_Data.Entities
{
    public class UserFile : BaseFile
    {
        [Required]
        public int UserId { get; set; }

        public bool IsProfilePhoto { get; set; }
        public bool IsCoverPhoto { get; set; }
        public bool IsCV { get; set; }

        [ForeignKey("UserId")]
        public User? ParentUser { get; set; }
    }
}
