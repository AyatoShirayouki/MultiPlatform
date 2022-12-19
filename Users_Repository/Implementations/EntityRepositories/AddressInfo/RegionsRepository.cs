using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities.AddressInfo;

namespace Users_Repository.Implementations.EntityRepositories.AddressInfo
{
    public class RegionsRepository : UsersBaseRepository<Region>
    {
        public RegionsRepository(UsersUnitOfWork uow) : base(uow)
        {

        }
        public RegionsRepository() : base()
        {

        }
        protected override IQueryable<Region> CascadeInclude(IQueryable<Region> query)
        {
            return query.Include(c => c.ParentCountry);
        }
    }
}
