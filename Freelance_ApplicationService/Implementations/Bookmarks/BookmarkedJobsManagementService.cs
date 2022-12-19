using Base.ManagementService;
using Freelance_ApplicationService.DTOs.Bookmarks;
using Freelance_Data.Entities.Bookmarks;
using Freelance_Repository.Implementations;
using Freelance_Repository.Implementations.EntityRepositories.Bookmarks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.Implementations.Bookmarks
{
    public class BookmarkedJobsManagementService : IBaseManagementService
    {
        public static async Task<List<BookmarkedJobDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                BookmarkedJobsRepository BookmarkedJobsRepo = new BookmarkedJobsRepository(unitOfWork);
                List<BookmarkedJob> BookmarkedJobs = await BookmarkedJobsRepo.GetAll();

                List<BookmarkedJobDTO> BookmarkedJobsDTO = new List<BookmarkedJobDTO>();

                if (BookmarkedJobs != null)
                {
                    foreach (var item in BookmarkedJobs)
                    {
                        BookmarkedJobsDTO.Add(new BookmarkedJobDTO
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            JobId = item.JobId
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return BookmarkedJobsDTO;
            }
        }

        public static async Task<BookmarkedJobDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                BookmarkedJobsRepository BookmarkedJobsRepo = new BookmarkedJobsRepository(unitOfWork);
                BookmarkedJobDTO BookmarkedJobDTO = new BookmarkedJobDTO();

                BookmarkedJob BookmarkedJob = await BookmarkedJobsRepo.GetById(id);

                if (BookmarkedJob != null)
                {
                    BookmarkedJobDTO.Id = BookmarkedJob.Id;
                    BookmarkedJobDTO.UserId = BookmarkedJob.UserId;
                    BookmarkedJobDTO.JobId = BookmarkedJob.JobId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return BookmarkedJobDTO;
            }
        }

        public static async Task Save(BookmarkedJobDTO BookmarkedJobDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                BookmarkedJobsRepository BookmarkedJobsRepo = new BookmarkedJobsRepository(unitOfWork);
                BookmarkedJob BookmarkedJob = new BookmarkedJob();

                if (BookmarkedJobDTO != null)
                {
                    if (BookmarkedJobDTO.Id == 0)
                    {
                        BookmarkedJob = new BookmarkedJob
                        {
                            UserId = BookmarkedJobDTO.UserId,
                            JobId = BookmarkedJobDTO.JobId
                        };
                    }
                    else
                    {
                        BookmarkedJob = new BookmarkedJob
                        {
                            Id = BookmarkedJobDTO.Id,
                            UserId = BookmarkedJobDTO.UserId,
                            JobId = BookmarkedJobDTO.JobId
                        };
                    }

                    await BookmarkedJobsRepo.Save(BookmarkedJob);
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

                BookmarkedJobsRepository BookmarkedJobsRepo = new BookmarkedJobsRepository(unitOfWork);
                BookmarkedJob BookmarkedJob = await BookmarkedJobsRepo.GetById(id);

                if (BookmarkedJob != null)
                {
                    await BookmarkedJobsRepo.Delete(BookmarkedJob); 
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
