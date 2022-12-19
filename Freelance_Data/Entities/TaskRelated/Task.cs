using Base.Entities;
using Freelance_Data.Entities.Others;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Data.Entities.TaskRelated
{
    public class Task : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime DateOfPosting { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ProjectName { get; set; }

        [MaxLength(150)]
        public string? Location { get; set; }

        [Required]
        public decimal? MinPrice { get; set; }

        [Required]
        public decimal? MaxPrice { get; set; }

        [Required]
        public decimal? TaskType { get; set; }

        [Required]
        [MaxLength(2000)]
        public string? Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? ParentCategory { get; set; }
    }
}
