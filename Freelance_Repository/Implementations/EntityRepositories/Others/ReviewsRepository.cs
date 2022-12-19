using Freelance_Data.Entities.JobRelated;
using Freelance_Data.Entities.Others;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.Others
{
    public class ReviewsRepository : FreelanceBaseRepository<Review>
    {
        public ReviewsRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public ReviewsRepository() : base()
        {

        }
        protected override IQueryable<Review> CascadeInclude(IQueryable<Review> query)
        {
            return query.Include(c => c.ParentTask);
        }
    }
}
