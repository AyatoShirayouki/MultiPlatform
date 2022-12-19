using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Entities
{
    public class BaseFile : BaseEntity
    {
        [Required]
        [MaxLength(300)]
        public string? FileName { get; set; }
        [Required]
        [MaxLength(15)]
        public string? FileType { get; set; }
    }
}
