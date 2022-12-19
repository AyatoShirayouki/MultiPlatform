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
    public class BookmarkedTasksManagementService : IBaseManagementService
    {
        public static async Task<List<BookmarkedTaskDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                BookmarkedTasksRepository BookmarkedTasksRepo = new BookmarkedTasksRepository(unitOfWork);
                List<BookmarkedTask> BookmarkedTasks = await BookmarkedTasksRepo.GetAll();

                List<BookmarkedTaskDTO> BookmarkedTasksDTO = new List<BookmarkedTaskDTO>();

                if (BookmarkedTasks != null)
                {
                    foreach (var item in BookmarkedTasks)
                    {
                        BookmarkedTasksDTO.Add(new BookmarkedTaskDTO
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            TaskId = item.TaskId
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return BookmarkedTasksDTO;
            }
        }

        public static async Task<BookmarkedTaskDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                BookmarkedTasksRepository BookmarkedTasksRepo = new BookmarkedTasksRepository(unitOfWork);
                BookmarkedTaskDTO BookmarkedTaskDTO = new BookmarkedTaskDTO();

                BookmarkedTask BookmarkedTask = await BookmarkedTasksRepo.GetById(id);

                if (BookmarkedTask != null)
                {
                    BookmarkedTaskDTO.Id = BookmarkedTask.Id;
                    BookmarkedTaskDTO.UserId = BookmarkedTask.UserId;
                    BookmarkedTaskDTO.TaskId = BookmarkedTask.TaskId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return BookmarkedTaskDTO;
            }
        }

        public static async Task Save(BookmarkedTaskDTO BookmarkedTaskDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                BookmarkedTasksRepository BookmarkedTasksRepo = new BookmarkedTasksRepository(unitOfWork);
                BookmarkedTask BookmarkedTask = new BookmarkedTask();

                if (BookmarkedTaskDTO != null)
                {
                    if (BookmarkedTaskDTO.Id == 0)
                    {
                        BookmarkedTask = new BookmarkedTask
                        {
                            UserId = BookmarkedTaskDTO.UserId,
                            TaskId = BookmarkedTaskDTO.TaskId
                        };
                    }
                    else
                    {
                        BookmarkedTask = new BookmarkedTask
                        {
                            Id = BookmarkedTaskDTO.Id,
                            UserId = BookmarkedTaskDTO.UserId,
                            TaskId = BookmarkedTaskDTO.TaskId
                        };
                    }

                    await BookmarkedTasksRepo.Save(BookmarkedTask);
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

                BookmarkedTasksRepository BookmarkedTasksRepo = new BookmarkedTasksRepository(unitOfWork);
                BookmarkedTask BookmarkedTask = await BookmarkedTasksRepo.GetById(id);

                if (BookmarkedTask != null)
                {
                    await BookmarkedTasksRepo.Delete(BookmarkedTask);
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
