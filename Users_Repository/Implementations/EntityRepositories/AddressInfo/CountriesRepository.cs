using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities.AddressInfo;

namespace Users_Repository.Implementations.EntityRepositories.AddressInfo
{
    public class CountriesRepository : UsersBaseRepository<Country>
    {
        public CountriesRepository(UsersUnitOfWork uow) : base(uow)
        {

        }
        public CountriesRepository() : base()
        {

        }
    }
}
