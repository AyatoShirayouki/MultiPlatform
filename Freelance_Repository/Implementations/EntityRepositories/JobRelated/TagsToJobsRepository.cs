using Freelance_Data.Entities.JobRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.JobRelated
{
    public class TagsToJobsRepository : FreelanceBaseRepository<TagToJob>
    {
        public TagsToJobsRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public TagsToJobsRepository() : base()
        {

        }
        protected override IQueryable<TagToJob> CascadeInclude(IQueryable<TagToJob> query)
        {
            return query.Include(c => c.ParentJob)
                .Include(c => c.ParentTag);
        }
    }
}
