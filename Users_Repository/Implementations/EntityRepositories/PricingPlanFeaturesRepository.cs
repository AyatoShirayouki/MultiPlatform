using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities;

namespace Users_Repository.Implementations.EntityRepositories
{
    public class PricingPlanFeaturesRepository : UsersBaseRepository<PricingPlanFeature>
    {
        public PricingPlanFeaturesRepository(UsersUnitOfWork uow) : base(uow)
        {

        }
        public PricingPlanFeaturesRepository() : base()
        {

        }
        protected override IQueryable<PricingPlanFeature> CascadeInclude(IQueryable<PricingPlanFeature> query)
        {
            return query.Include(c => c.ParentPricingPlan);
        }
    }
}
