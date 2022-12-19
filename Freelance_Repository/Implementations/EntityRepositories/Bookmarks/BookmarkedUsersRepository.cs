using Freelance_Data.Entities.Bookmarks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Repository.Implementations.EntityRepositories.Bookmarks
{
    public class BookmarkedUsersRepository : FreelanceBaseRepository<BookmarkedUser>
    {
        public BookmarkedUsersRepository(FreelanceUnitOfWork uow) : base(uow)
        {

        }
        public BookmarkedUsersRepository() : base()
        {

        }
    }
}
