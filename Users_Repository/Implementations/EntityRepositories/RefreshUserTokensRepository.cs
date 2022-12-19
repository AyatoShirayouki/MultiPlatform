using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities;

namespace Users_Repository.Implementations.EntityRepositories
{
    public class RefreshUserTokensRepository : UsersBaseRepository<RefreshUserToken>
    {
        public RefreshUserTokensRepository(UsersUnitOfWork uow) : base(uow)
        {

        }
        public RefreshUserTokensRepository() : base()
        {

        }
        protected override IQueryable<RefreshUserToken> CascadeInclude(IQueryable<RefreshUserToken> query)
        {
            return query.Include(c => c.ParentUser);
        }
    }
}
