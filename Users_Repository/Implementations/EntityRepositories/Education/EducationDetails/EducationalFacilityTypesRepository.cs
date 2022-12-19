using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities.Education.EducationDetails;

namespace Users_Repository.Implementations.EntityRepositories.Education.EducationDetails
{
    public class EducationalFacilityTypesRepository : UsersBaseRepository<EducationalFacilityType>
    {
        public EducationalFacilityTypesRepository(UsersUnitOfWork uow) : base(uow)
        {

        }
        public EducationalFacilityTypesRepository() : base()
        {

        }
    }
}
