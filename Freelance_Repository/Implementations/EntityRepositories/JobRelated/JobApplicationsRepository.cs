using Freelance_Data.Entities.JobRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.JobRelated
{
    public class JobApplicationsRepository : FreelanceBaseRepository<JobApplication>
    {
        public JobApplicationsRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public JobApplicationsRepository() : base()
        {

        }
        protected override IQueryable<JobApplication> CascadeInclude(IQueryable<JobApplication> query)
        {
            return query.Include(c => c.ParentJob);
        }
    }
}
