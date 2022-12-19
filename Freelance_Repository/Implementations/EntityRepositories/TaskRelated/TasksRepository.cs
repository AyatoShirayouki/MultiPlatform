using Freelance_Data.Entities.TaskRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Freelance_Data.Entities.TaskRelated.Task;

namespace Freelance_Repository.Implementations.EntityRepositories.TaskRelated
{
    public class TasksRepository : FreelanceBaseRepository<Task>
    {
        public TasksRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public TasksRepository() : base()
        {

        }
        protected override IQueryable<Task> CascadeInclude(IQueryable<Task> query)
        {
            return query.Include(c => c.ParentCategory);
        }
    }
}
