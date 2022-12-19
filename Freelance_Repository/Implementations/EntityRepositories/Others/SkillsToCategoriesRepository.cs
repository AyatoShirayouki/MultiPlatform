using Freelance_Data.Entities.Others;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.Others
{
    public class SkillsToCategoriesRepository : FreelanceBaseRepository<SkillToCategory>
    {
        public SkillsToCategoriesRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public SkillsToCategoriesRepository() : base()
        {

        }
        protected override IQueryable<SkillToCategory> CascadeInclude(IQueryable<SkillToCategory> query)
        {
            return query.Include(c => c.ParentCategory)
                .Include(c => c.ParentSkill);
        }
    }
}
