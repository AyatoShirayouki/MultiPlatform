using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities.AddressInfo;

namespace Users_Repository.Implementations.EntityRepositories.AddressInfo
{
    public class AddressesRepository : UsersBaseRepository<Address>
    {
        public AddressesRepository(UsersUnitOfWork uow) : base(uow)
        {

        }
        public AddressesRepository() : base()
        {

        }
        protected override IQueryable<Address> CascadeInclude(IQueryable<Address> query)
        {
            return query.Include(c => c.ParentCountry)
                .Include(c => c.ParentRegion)
                .Include(c => c.ParentCity);
        }
    }
}
