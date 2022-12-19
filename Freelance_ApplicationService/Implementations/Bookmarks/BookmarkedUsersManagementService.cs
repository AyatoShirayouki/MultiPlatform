using Base.ManagementService;
using Freelance_ApplicationService.DTOs.Bookmarks;
using Freelance_Data.Entities.Bookmarks;
using Freelance_Repository.Implementations.EntityRepositories.Bookmarks;
using Freelance_Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.Implementations.Bookmarks
{
    public class BookmarkedUsersManagementService : IBaseManagementService
    {
        public static async Task<List<BookmarkedUserDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                BookmarkedUsersRepository BookmarkedUsersRepo = new BookmarkedUsersRepository(unitOfWork);
                List<BookmarkedUser> BookmarkedUsers = await BookmarkedUsersRepo.GetAll();

                List<BookmarkedUserDTO> BookmarkedUsersDTO = new List<BookmarkedUserDTO>();

                if (BookmarkedUsers != null)
                {
                    foreach (var item in BookmarkedUsers)
                    {
                        BookmarkedUsersDTO.Add(new BookmarkedUserDTO
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            BookmarkedUserId = item.BookmarkedUserId
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return BookmarkedUsersDTO;
            }
        }

        public static async Task<BookmarkedUserDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                BookmarkedUsersRepository BookmarkedUsersRepo = new BookmarkedUsersRepository(unitOfWork);
                BookmarkedUserDTO BookmarkedUserDTO = new BookmarkedUserDTO();

                BookmarkedUser BookmarkedUser = await BookmarkedUsersRepo.GetById(id);

                if (BookmarkedUser != null)
                {
                    BookmarkedUserDTO.Id = BookmarkedUser.Id;
                    BookmarkedUserDTO.UserId = BookmarkedUser.UserId;
                    BookmarkedUserDTO.BookmarkedUserId = BookmarkedUser.BookmarkedUserId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return BookmarkedUserDTO;
            }
        }

        public static async Task Save(BookmarkedUserDTO BookmarkedUserDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                BookmarkedUsersRepository BookmarkedUsersRepo = new BookmarkedUsersRepository(unitOfWork);
                BookmarkedUser BookmarkedUser = new BookmarkedUser();

                if (BookmarkedUserDTO != null)
                {
                    if (BookmarkedUserDTO.Id == 0)
                    {
                        BookmarkedUser = new BookmarkedUser
                        {
                            UserId = BookmarkedUserDTO.UserId,
                            BookmarkedUserId = BookmarkedUserDTO.BookmarkedUserId
                        };
                    }
                    else
                    {
                        BookmarkedUser = new BookmarkedUser
                        {
                            Id = BookmarkedUserDTO.Id,
                            UserId = BookmarkedUserDTO.UserId,
                            BookmarkedUserId = BookmarkedUserDTO.BookmarkedUserId
                        };
                    }

                    await BookmarkedUsersRepo.Save(BookmarkedUser);
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
            }
        }

        public static async Task Delete(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                BookmarkedUsersRepository BookmarkedUsersRepo = new BookmarkedUsersRepository(unitOfWork);
                BookmarkedUser BookmarkedUser = await BookmarkedUsersRepo.GetById(id);

                if (BookmarkedUser != null)
                {
                    await BookmarkedUsersRepo.Delete(BookmarkedUser);
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
            }
        }
    }
}
