using Freelance_Data.Entities.Others;
using Freelance_Data.Entities.TaskRelated;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.TaskRelated
{
    public class FilesToTasksRepository : FreelanceBaseRepository<FileToTask>
    {
        public FilesToTasksRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public FilesToTasksRepository() : base()
        {

        }
        protected override IQueryable<FileToTask> CascadeInclude(IQueryable<FileToTask> query)
        {
            return query.Include(c => c.ParentTask)
                .Include(c => c.ParentFreelanceFile);
        }
    }
}
