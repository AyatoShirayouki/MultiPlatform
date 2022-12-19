using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities;

namespace Users_Repository.Implementations.EntityRepositories
{
    public class UsersRepository : UsersBaseRepository<User>
    {
        public UsersRepository(UsersUnitOfWork uow) : base(uow)
        {

        }
        public UsersRepository() : base()
        {

        }
        protected override IQueryable<User> CascadeInclude(IQueryable<User> query)
        {
            return query.Include(c => c.ParentAddress)
                .Include(c => c.ParentPricingPlan);
        }
    }
}
