using Freelance_Data.Entities.Bookmarks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.Bookmarks
{
    public class BookmarkedJobsRepository : FreelanceBaseRepository<BookmarkedJob>
    {
        public BookmarkedJobsRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public BookmarkedJobsRepository() : base()
        {

        }
        protected override IQueryable<BookmarkedJob> CascadeInclude(IQueryable<BookmarkedJob> query)
        {
            return query.Include(c => c.ParentJob);
        }
    }
}
