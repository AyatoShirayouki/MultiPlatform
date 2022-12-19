using Admins_Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admins_Repository.Implementations.EntityRepositories
{
    public class AdminFilesRepository : AdminsBaseRepository<AdminFile>
    {
        public AdminFilesRepository(AdminsUnitOfWork uow) : base(uow)
        {

        }
        public AdminFilesRepository() : base()
        {

        }
        protected override IQueryable<AdminFile> CascadeInclude(IQueryable<AdminFile> query)
        {
            return query.Include(c => c.ParentAdmin);
        }
    }
}
