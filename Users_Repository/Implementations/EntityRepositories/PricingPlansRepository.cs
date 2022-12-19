using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities;

namespace Users_Repository.Implementations.EntityRepositories
{
    public class PricingPlansRepository : UsersBaseRepository<PricingPlan>
    {
        public PricingPlansRepository(UsersUnitOfWork uow) : base(uow)
        {

        }
        public PricingPlansRepository() : base()
        {

        }
    }
}
