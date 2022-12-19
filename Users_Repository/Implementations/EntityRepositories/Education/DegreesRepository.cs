using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities.Education;

namespace Users_Repository.Implementations.EntityRepositories.Education
{
    public class DegreesRepository : UsersBaseRepository<Degree>
    {
        public DegreesRepository(UsersUnitOfWork uow) : base(uow)
        {

        }
        public DegreesRepository() : base()
        {

        }
    }
}
