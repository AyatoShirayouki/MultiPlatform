using Freelance_Data.Entities.Bookmarks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.Bookmarks
{
    public class BookmarkedTasksRepository : FreelanceBaseRepository<BookmarkedTask>
    {
        public BookmarkedTasksRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public BookmarkedTasksRepository() : base()
        {

        }
        protected override IQueryable<BookmarkedTask> CascadeInclude(IQueryable<BookmarkedTask> query)
        {
            return query.Include(c => c.ParentTask);
        }
    }
}
