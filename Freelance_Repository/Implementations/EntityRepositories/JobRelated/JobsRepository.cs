using Freelance_Data.Entities.JobRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.JobRelated
{
    public class JobsRepository : FreelanceBaseRepository<Job>
    {
        public JobsRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public JobsRepository() : base()
        {

        }
        protected override IQueryable<Job> CascadeInclude(IQueryable<Job> query)
        {
            return query.Include(c => c.ParentCategory)
                .Include(c => c.ParentJobType);
        }
    }
}
