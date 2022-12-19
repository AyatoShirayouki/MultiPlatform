using Freelance_Data.Entities.TaskRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.TaskRelated
{
    public class SkillsToTasksRepository : FreelanceBaseRepository<SkillToTask>
    {
        public SkillsToTasksRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public SkillsToTasksRepository() : base()
        {

        }
        protected override IQueryable<SkillToTask> CascadeInclude(IQueryable<SkillToTask> query)
        {
            return query.Include(c => c.ParentSkill)
                .Include(c => c.ParentTask);
        }
    }
}
