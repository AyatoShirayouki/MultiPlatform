using Freelance_Data.Entities.JobRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.JobRelated
{
    public class SkillsToJobsRepository : FreelanceBaseRepository<SkillToJob>
    {
        public SkillsToJobsRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public SkillsToJobsRepository() : base()
        {

        }
        protected override IQueryable<SkillToJob> CascadeInclude(IQueryable<SkillToJob> query)
        {
            return query.Include(c => c.ParentJob)
                .Include(c => c.ParentSkill);
        }
    }
}
