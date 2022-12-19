using Freelance_Data.Entities.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.Others
{
    public class FreelanceFilesRepository : FreelanceBaseRepository<FreelanceFile>
    {
        public FreelanceFilesRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public FreelanceFilesRepository() : base()
        {

        }
    }
}
