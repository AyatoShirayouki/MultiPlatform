using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.DTOs.Bookmarks
{
    public class BookmarkedTaskDTO : BaseEntity
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
    }
}
