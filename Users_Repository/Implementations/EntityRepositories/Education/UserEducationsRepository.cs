using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities;
using Users_Data.Entities.Education;

namespace Users_Repository.Implementations.EntityRepositories.Education
{
    public class UserEducationsRepository : UsersBaseRepository<UserEducation>
    {
        public UserEducationsRepository(UsersUnitOfWork uow) : base(uow)
        {

        }
        public UserEducationsRepository() : base()
        {

        }
        protected override IQueryable<UserEducation> CascadeInclude(IQueryable<UserEducation> query)
        {
            return query.Include(c => c.ParentUser)
                .Include(c => c.ParentDegree)
                .Include(c => c.ParentAcademicField)
                .Include(c => c.ParentEducationalFacility);
        }
    }
}
