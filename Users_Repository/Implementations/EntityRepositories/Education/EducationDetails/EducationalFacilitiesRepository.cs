using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities.Education;
using Users_Data.Entities.Education.EducationDetails;

namespace Users_Repository.Implementations.EntityRepositories.Education.EducationDetails
{
    public class EducationalFacilitiesRepository : UsersBaseRepository<EducationalFacility>
    {
        public EducationalFacilitiesRepository(UsersUnitOfWork uow) : base(uow)
        {

        }
        public EducationalFacilitiesRepository() : base()
        {

        }
        protected override IQueryable<EducationalFacility> CascadeInclude(IQueryable<EducationalFacility> query)
        {
            return query.Include(c => c.ParentEducationalFacilityType)
                .Include(c => c.ParentCountry);
        }
    }
}
