using Freelance_Data.Entities.Bookmarks;
using Freelance_Data.Entities.JobRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.JobRelated
{
    public class FilesToJobsRepository : FreelanceBaseRepository<FileToJob>
    {
        public FilesToJobsRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public FilesToJobsRepository() : base()
        {

        }
        protected override IQueryable<FileToJob> CascadeInclude(IQueryable<FileToJob> query)
        {
            return query.Include(c => c.ParentJob)
                .Include(c => c.ParentFreelanceFile);
        }
    }
}
