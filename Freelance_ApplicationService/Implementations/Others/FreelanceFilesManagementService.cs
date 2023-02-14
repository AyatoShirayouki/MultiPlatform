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
using Freelance_ApplicationService.DTOs.Others;
using Freelance_Repository.Implementations.EntityRepositories.Others;
using Freelance_Data.Entities.Others;
using Freelance_Repository.Implementations.EntityRepositories.JobRelated;
using Freelance_Repository.Implementations.EntityRepositories.TaskRelated;
using Freelance_Data.Entities.JobRelated;
using Freelance_Data.Entities.TaskRelated;

namespace Freelance_ApplicationService.Implementations.Others
{
    public class FreelanceFilesManagementService : IBaseManagementService
    {
        public static async Task<List<FreelanceFileDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                FreelanceFilesRepository FreelanceFilesRepo = new FreelanceFilesRepository(unitOfWork);
                List<FreelanceFile> FreelanceFiles = await FreelanceFilesRepo.GetAll();

                List<FreelanceFileDTO> FreelanceFilesDTO = new List<FreelanceFileDTO>();

                if (FreelanceFiles != null)
                {
                    foreach (var item in FreelanceFiles)
                    {
                        FreelanceFilesDTO.Add(new FreelanceFileDTO
                        {
                            Id = item.Id,
                            FileName = item.FileName,
                            FileType = item.FileType
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return FreelanceFilesDTO;
            }
        }

        public static async Task<FreelanceFileDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                FreelanceFilesRepository FreelanceFilesRepo = new FreelanceFilesRepository(unitOfWork);
                FreelanceFileDTO FreelanceFileDTO = new FreelanceFileDTO();

                FreelanceFile FreelanceFile = await FreelanceFilesRepo.GetById(id);

                if (FreelanceFile != null)
                {
                    FreelanceFileDTO.Id = FreelanceFile.Id;
                    FreelanceFileDTO.FileName = FreelanceFile.FileName;
                    FreelanceFileDTO.FileType = FreelanceFile.FileType;
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return FreelanceFileDTO;
            }
        }

        public static async System.Threading.Tasks.Task Save(FreelanceFileDTO FreelanceFileDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                FreelanceFilesRepository FreelanceFilesRepo = new FreelanceFilesRepository(unitOfWork);
                FreelanceFile FreelanceFile = new FreelanceFile();

                if (FreelanceFileDTO != null)
                {
                    if (FreelanceFileDTO.Id == 0)
                    {
                        FreelanceFile = new FreelanceFile
                        {
                            FileName = FreelanceFileDTO.FileName,
                            FileType = FreelanceFileDTO.FileType
                        };
                    }
                    else
                    {
                        FreelanceFile = new FreelanceFile
                        {
                            Id = FreelanceFileDTO.Id,
                            FileName = FreelanceFileDTO.FileName,
                            FileType = FreelanceFileDTO.FileType
                        };
                    }

                    await FreelanceFilesRepo.Save(FreelanceFile);
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
            }
        }

        public static async System.Threading.Tasks.Task Delete(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                FilesToJobsRepository filesToJobsRepo = new FilesToJobsRepository(unitOfWork);
                FilesToTasksRepository filesToTasksRepo = new FilesToTasksRepository(unitOfWork);
                FreelanceFilesRepository FreelanceFilesRepo = new FreelanceFilesRepository(unitOfWork);

                FreelanceFile FreelanceFile = await FreelanceFilesRepo.GetById(id);

                if (FreelanceFile != null)
                {
                    List<FileToJob> filesToJobs = await filesToJobsRepo.GetAll(c => c.FileId == FreelanceFile.Id);
                    List<FileToTask> filesToTasks = await filesToTasksRepo.GetAll(c => c.FileId == FreelanceFile.Id);

                    foreach (FileToJob fileToJob in filesToJobs)
                    {
                        await filesToJobsRepo.Delete(fileToJob);
                    }

                    foreach (var fileToTask in filesToTasks)
                    {
                        await filesToTasksRepo.Delete(fileToTask);  
                    }

                    await FreelanceFilesRepo.Delete(FreelanceFile);
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
