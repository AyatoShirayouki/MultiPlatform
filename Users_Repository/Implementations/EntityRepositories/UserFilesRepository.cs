using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities;

namespace Users_Repository.Implementations.EntityRepositories
{
    public class UserFilesRepository : UsersBaseRepository<UserFile>
    {
        public UserFilesRepository(UsersUnitOfWork uow) : base(uow)
        {

        }
        public UserFilesRepository() : base()
        {

        }
        protected override IQueryable<UserFile> CascadeInclude(IQueryable<UserFile> query)
        {
            return query.Include(c => c.ParentUser);
        }
    }
}
