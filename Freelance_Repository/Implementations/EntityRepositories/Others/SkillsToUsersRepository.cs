using Freelance_Data.Entities.Bookmarks;
using Freelance_Data.Entities.Others;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.Others
{
	public class SkillsToUsersRepository : FreelanceBaseRepository<SkillToUser>
	{
		public SkillsToUsersRepository(FreelanceUnitOfWork uow) : base(uow)
		{

		}
		public SkillsToUsersRepository() : base()
		{

		}
		protected override IQueryable<SkillToUser> CascadeInclude(IQueryable<SkillToUser> query)
		{
			return query.Include(c => c.ParentSkill);
		}
	}
}
