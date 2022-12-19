using Freelance_Data.Entities.TaskRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.TaskRelated
{
    public class TaskBidsRepository : FreelanceBaseRepository<TaskBid>
    {
        public TaskBidsRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public TaskBidsRepository() : base()
        {

        }
        protected override IQueryable<TaskBid> CascadeInclude(IQueryable<TaskBid> query)
        {
            return query.Include(c => c.ParentTask);
        }
    }
}
