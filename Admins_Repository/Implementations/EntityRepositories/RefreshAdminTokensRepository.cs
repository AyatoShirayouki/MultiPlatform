using Admins_Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admins_Repository.Implementations.EntityRepositories
{
    public class RefreshAdminTokensRepository : AdminsBaseRepository<RefreshAdminToken>
    {
        public RefreshAdminTokensRepository(AdminsUnitOfWork uow) : base(uow)
        {

        }
        public RefreshAdminTokensRepository() : base()
        {

        }
        protected override IQueryable<RefreshAdminToken> CascadeInclude(IQueryable<RefreshAdminToken> query)
        {
            return query.Include(c => c.ParentAdmin);
        }
    }
}

