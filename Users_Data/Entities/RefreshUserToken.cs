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
    public class RefreshUserToken : BaseToken
    {
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? ParentUser { get; set; }
    }
}
