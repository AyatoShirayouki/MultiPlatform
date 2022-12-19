using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.DTOs.Others
{
    public class NoteDTO : BaseEntity
    {
        public int UserId { get; set; }
        public string? Content { get; set; }
        public int Priority { get; set; }
    }
}
