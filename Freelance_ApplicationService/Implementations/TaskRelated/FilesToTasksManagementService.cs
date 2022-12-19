using Base.ManagementService;
using Freelance_ApplicationService.DTOs.Others;
using Freelance_Data.Entities.Others;
using Freelance_Repository.Implementations.EntityRepositories.Others;
using Freelance_Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Freelance_ApplicationService.DTOs.TaskRelated;
using Freelance_Data.Entities.TaskRelated;
using Freelance_Repository.Implementations.EntityRepositories.TaskRelated;

namespace Freelance_ApplicationService.Implementations.TaskRelated
{
    public class FilesToTasksManagementService : IBaseManagementService
    {
        public static async Task<List<FileToTaskDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                FilesToTasksRepository FileToTasksRepo = new FilesToTasksRepository(unitOfWork);
                List<FileToTask> FileToTasks = await FileToTasksRepo.GetAll();

                List<FileToTaskDTO> FileToTasksDTO = new List<FileToTaskDTO>();

                if (FileToTasks != null)
                {
                    foreach (var item in FileToTasks)
                    {
                        FileToTasksDTO.Add(new FileToTaskDTO
                        {
                            Id = item.Id,
                            FileId = item.FileId,
                            TaskId = item.TaskId
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return FileToTasksDTO;
            }
        }

        public static async Task<FileToTaskDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                FilesToTasksRepository FileToTasksRepo = new FilesToTasksRepository(unitOfWork);
                FileToTaskDTO FileToTaskDTO = new FileToTaskDTO();

                FileToTask FileToTask = await FileToTasksRepo.GetById(id);

                if (FileToTask != null)
                {
                    FileToTaskDTO.Id = FileToTask.Id;
                    FileToTaskDTO.FileId = FileToTask.FileId;
                    FileToTaskDTO.TaskId = FileToTask.TaskId;
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return FileToTaskDTO;
            }
        }

        public static async System.Threading.Tasks.Task Save(FileToTaskDTO FileToTaskDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                FilesToTasksRepository FileToTasksRepo = new FilesToTasksRepository(unitOfWork);
                FileToTask FileToTask = new FileToTask();

                if (FileToTaskDTO != null)
                {
                    if (FileToTaskDTO.Id == 0)
                    {
                        FileToTask = new FileToTask
                        {
                            FileId = FileToTaskDTO.FileId,
                            TaskId = FileToTaskDTO.TaskId
                        };
                    }
                    else
                    {
                        FileToTask = new FileToTask
                        {
                            Id = FileToTaskDTO.Id,
                            TaskId = FileToTaskDTO.TaskId,
                            FileId = FileToTaskDTO.FileId
                        };
                    }

                    await FileToTasksRepo.Save(FileToTask);
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

                FilesToTasksRepository FileToTasksRepo = new FilesToTasksRepository(unitOfWork);
                FileToTask FileToTask = await FileToTasksRepo.GetById(id);

                if (FileToTask != null)
                {
                    await FileToTasksRepo.Delete(FileToTask);
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
