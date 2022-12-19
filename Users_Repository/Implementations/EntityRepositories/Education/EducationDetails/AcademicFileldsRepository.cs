using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities.Education.EducationDetails;

namespace Users_Repository.Implementations.EntityRepositories.Education.EducationDetails
{
    public class AcademicFileldsRepository : UsersBaseRepository<AcademicField>
    {
        public AcademicFileldsRepository(UsersUnitOfWork uow) : base(uow)
        {

        }
        public AcademicFileldsRepository() : base()
        {

        }
    }
}
