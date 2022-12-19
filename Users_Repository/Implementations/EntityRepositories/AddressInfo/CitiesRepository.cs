using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities.AddressInfo;

namespace Users_Repository.Implementations.EntityRepositories.AddressInfo
{
    public class CitiesRepository : UsersBaseRepository<City>
    {
        public CitiesRepository(UsersUnitOfWork uow) : base(uow)
        {

        }
        public CitiesRepository() : base()
        {

        }
        protected override IQueryable<City> CascadeInclude(IQueryable<City> query)
        {
            return query.Include(c => c.ParentRegion);
        }
    }
}
