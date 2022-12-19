using Admins_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admins_Repository.Implementations.EntityRepositories
{
    public class AdminsRepository : AdminsBaseRepository<Admin>
    {
        public AdminsRepository(AdminsUnitOfWork uow) : base(uow)
        {

        }
        public AdminsRepository() : base()
        {

        }
    }
}