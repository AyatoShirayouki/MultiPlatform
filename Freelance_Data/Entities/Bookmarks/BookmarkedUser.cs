using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Data.Entities.Bookmarks
{
    public class BookmarkedUser : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookmarkedUserId { get; set; }
    }
}
