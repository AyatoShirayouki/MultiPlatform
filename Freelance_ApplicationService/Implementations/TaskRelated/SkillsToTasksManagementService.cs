using Base.ManagementService;
using Freelance_ApplicationService.DTOs.TaskRelated;
using Freelance_Data.Entities.TaskRelated;
using Freelance_Repository.Implementations.EntityRepositories.TaskRelated;
using Freelance_Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.Implementations.TaskRelated
{
    public class SkillsToTasksManagementService : IBaseManagementService
    {
        public static async Task<List<SkillToTaskDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                SkillsToTasksRepository SkillToTasksRepo = new SkillsToTasksRepository(unitOfWork);
                List<SkillToTask> SkillToTasks = await SkillToTasksRepo.GetAll();

                List<SkillToTaskDTO> SkillToTasksDTO = new List<SkillToTaskDTO>();

                if (SkillToTasks != null)
                {
                    foreach (var item in SkillToTasks)
                    {
                        SkillToTasksDTO.Add(new SkillToTaskDTO
                        {
                            Id = item.Id,
                            SkillId = item.SkillId,
                            TaskId = item.TaskId
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return SkillToTasksDTO;
            }
        }

        public static async Task<SkillToTaskDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                SkillsToTasksRepository SkillToTasksRepo = new SkillsToTasksRepository(unitOfWork);
                SkillToTaskDTO SkillToTaskDTO = new SkillToTaskDTO();

                SkillToTask SkillToTask = await SkillToTasksRepo.GetById(id);

                if (SkillToTask != null)
                {
                    SkillToTaskDTO.Id = SkillToTask.Id;
                    SkillToTaskDTO.SkillId = SkillToTask.SkillId;
                    SkillToTaskDTO.TaskId = SkillToTask.TaskId;
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return SkillToTaskDTO;
            }
        }

        public static async System.Threading.Tasks.Task Save(SkillToTaskDTO SkillToTaskDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                SkillsToTasksRepository SkillToTasksRepo = new SkillsToTasksRepository(unitOfWork);
                SkillToTask SkillToTask = new SkillToTask();

                if (SkillToTaskDTO != null)
                {
                    if (SkillToTaskDTO.Id == 0)
                    {
                        SkillToTask = new SkillToTask
                        {
                            SkillId = SkillToTaskDTO.SkillId,
                            TaskId = SkillToTaskDTO.TaskId
                        };
                    }
                    else
                    {
                        SkillToTask = new SkillToTask
                        {
                            Id = SkillToTaskDTO.Id,
                            TaskId = SkillToTaskDTO.TaskId,
                            SkillId = SkillToTaskDTO.SkillId
                        };
                    }

                    await SkillToTasksRepo.Save(SkillToTask);
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

                SkillsToTasksRepository SkillToTasksRepo = new SkillsToTasksRepository(unitOfWork);
                SkillToTask SkillToTask = await SkillToTasksRepo.GetById(id);

                if (SkillToTask != null)
                {
                    await SkillToTasksRepo.Delete(SkillToTask);
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
