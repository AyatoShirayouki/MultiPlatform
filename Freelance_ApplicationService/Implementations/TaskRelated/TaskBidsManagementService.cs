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
    public class TaskBidsManagementService : IBaseManagementService
    {
        public static async Task<List<TaskBidDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                TaskBidsRepository TaskBidsRepo = new TaskBidsRepository(unitOfWork);
                List<TaskBid> TaskBids = await TaskBidsRepo.GetAll();

                List<TaskBidDTO> TaskBidsDTO = new List<TaskBidDTO>();

                if (TaskBids != null)
                {
                    foreach (var item in TaskBids)
                    {
                        TaskBidsDTO.Add(new TaskBidDTO
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            TaskId = item.TaskId,
                            DateOfBidding = item.DateOfBidding,
                            MinimalRate = item.MinimalRate,
                            DeliveryTimeCount = item.DeliveryTimeCount,
                            DeliveryTimeType = item.DeliveryTimeType,
                            Status = item.Status
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return TaskBidsDTO;
            }
        }

        public static async Task<TaskBidDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                TaskBidsRepository TaskBidsRepo = new TaskBidsRepository(unitOfWork);
                TaskBidDTO TaskBidDTO = new TaskBidDTO();

                TaskBid TaskBid = await TaskBidsRepo.GetById(id);

                if (TaskBid != null)
                {
                    TaskBidDTO.Id = TaskBid.Id;
                    TaskBidDTO.UserId = TaskBid.UserId;
                    TaskBidDTO.TaskId = TaskBid.TaskId;
                    TaskBidDTO.DateOfBidding = TaskBid.DateOfBidding;
                    TaskBidDTO.MinimalRate = TaskBid.MinimalRate;
                    TaskBidDTO.DeliveryTimeCount = TaskBid.DeliveryTimeCount;
                    TaskBidDTO.DeliveryTimeType = TaskBid.DeliveryTimeType;
                    TaskBidDTO.Status = TaskBid.Status;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return TaskBidDTO;
            }
        }

        public static async System.Threading.Tasks.Task Save(TaskBidDTO TaskBidDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                TaskBidsRepository TaskBidsRepo = new TaskBidsRepository(unitOfWork);
                TaskBid TaskBid = new TaskBid();

                if (TaskBidDTO != null)
                {
                    if (TaskBidDTO.Id == 0)
                    {
                        TaskBid = new TaskBid
                        {
                            UserId = TaskBidDTO.UserId,
                            TaskId = TaskBidDTO.TaskId,
                            DateOfBidding = TaskBidDTO.DateOfBidding,
                            MinimalRate = TaskBidDTO.MinimalRate,
                            DeliveryTimeCount = TaskBidDTO.DeliveryTimeCount,
                            DeliveryTimeType = TaskBidDTO.DeliveryTimeType,
                            Status = TaskBidDTO.Status
                        };
                    }
                    else
                    {
                        TaskBid = new TaskBid
                        {
                            Id = TaskBidDTO.Id,
                            UserId = TaskBidDTO.UserId,
                            TaskId = TaskBidDTO.TaskId,
                            DateOfBidding = TaskBidDTO.DateOfBidding,
                            MinimalRate = TaskBidDTO.MinimalRate,
                            DeliveryTimeCount = TaskBidDTO.DeliveryTimeCount,
                            DeliveryTimeType = TaskBidDTO.DeliveryTimeType,
                            Status = TaskBidDTO.Status
                        };
                    }

                    await TaskBidsRepo.Save(TaskBid);
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

                TaskBidsRepository TaskBidsRepo = new TaskBidsRepository(unitOfWork);
                TaskBid TaskBid = await TaskBidsRepo.GetById(id);

                if (TaskBid != null)
                {
                    await TaskBidsRepo.Delete(TaskBid);
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
