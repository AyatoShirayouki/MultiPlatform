using Freelance_Data.Entities.JobRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.JobRelated
{
    public class JobTypesRepository : FreelanceBaseRepository<JobType>
    {
        public JobTypesRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public JobTypesRepository() : base()
        {

        }
    }
}
