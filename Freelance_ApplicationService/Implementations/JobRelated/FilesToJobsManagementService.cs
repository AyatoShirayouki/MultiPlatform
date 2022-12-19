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
using Freelance_ApplicationService.DTOs.JobRelated;
using Freelance_Data.Entities.JobRelated;
using Freelance_Repository.Implementations.EntityRepositories.JobRelated;

namespace Freelance_ApplicationService.Implementations.JobRelated
{
    public class FilesToJobsManagementService : IBaseManagementService
    {
        public static async Task<List<FileToJobDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                FilesToJobsRepository FileToJobsRepo = new FilesToJobsRepository(unitOfWork);
                List<FileToJob> FileToJobs = await FileToJobsRepo.GetAll();

                List<FileToJobDTO> FileToJobsDTO = new List<FileToJobDTO>();

                if (FileToJobs != null)
                {
                    foreach (var item in FileToJobs)
                    {
                        FileToJobsDTO.Add(new FileToJobDTO
                        {
                            Id = item.Id,
                            FileId = item.FileId,
                            JobId = item.JobId
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return FileToJobsDTO;
            }
        }

        public static async Task<FileToJobDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                FilesToJobsRepository FileToJobsRepo = new FilesToJobsRepository(unitOfWork);
                FileToJobDTO FileToJobDTO = new FileToJobDTO();

                FileToJob FileToJob = await FileToJobsRepo.GetById(id);

                if (FileToJob != null)
                {
                    FileToJobDTO.Id = FileToJob.Id;
                    FileToJobDTO.FileId = FileToJob.FileId;
                    FileToJobDTO.JobId = FileToJob.JobId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return FileToJobDTO;
            }
        }

        public static async Task Save(FileToJobDTO FileToJobDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                FilesToJobsRepository FileToJobsRepo = new FilesToJobsRepository(unitOfWork);
                FileToJob FileToJob = new FileToJob();

                if (FileToJobDTO != null)
                {
                    if (FileToJobDTO.Id == 0)
                    {
                        FileToJob = new FileToJob
                        {
                            FileId = FileToJobDTO.FileId,
                            JobId = FileToJobDTO.JobId
                        };
                    }
                    else
                    {
                        FileToJob = new FileToJob
                        {
                            Id = FileToJobDTO.Id,
                            FileId = FileToJobDTO.FileId,
                            JobId = FileToJobDTO.JobId
                        };
                    }

                    await FileToJobsRepo.Save(FileToJob);
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

                FilesToJobsRepository FileToJobsRepo = new FilesToJobsRepository(unitOfWork);
                FileToJob FileToJob = await FileToJobsRepo.GetById(id);

                if (FileToJob != null)
                {
                    await FileToJobsRepo.Delete(FileToJob);
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
