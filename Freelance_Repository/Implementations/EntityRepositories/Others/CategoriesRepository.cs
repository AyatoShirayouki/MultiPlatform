using Freelance_Data.Entities.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.Others
{
    public class CategoriesRepository : FreelanceBaseRepository<Category>
    {
        public CategoriesRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public CategoriesRepository() : base()
        {

        }
    }
}
