using Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.DTOs.Others
{
	public class SkillToUserDTO : BaseEntity
	{
		public int UserId { get; set; }
		public int SkillId { get; set; }
	}
}
