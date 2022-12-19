using Base.ManagementService;
using Freelance_ApplicationService.DTOs.TaskRelated;
using Freelance_Data.Entities.TaskRelated;
using Freelance_Repository.Implementations.EntityRepositories.TaskRelated;
using Freelance_Repository.Implementations;
using Freelance_Repository.Implementations.EntityRepositories.Bookmarks;
using Freelance_Data.Entities.Bookmarks;

namespace Freelance_ApplicationService.Implementations.TaskRelated
{
    public class TasksManagementService : IBaseManagementService
    {
        public static async Task<List<TaskDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                TasksRepository TasksRepo = new TasksRepository(unitOfWork);
                List<Freelance_Data.Entities.TaskRelated.Task> Tasks = await TasksRepo.GetAll();

                List<TaskDTO> TasksDTO = new List<TaskDTO>();

                if (Tasks != null)
                {
                    foreach (var item in Tasks)
                    {
                        TasksDTO.Add(new TaskDTO
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            DateOfPosting = item.DateOfPosting,
                            ProjectName = item.ProjectName,
                            Location = item.Location,
                            MinPrice = item.MinPrice,
                            MaxPrice = item.MaxPrice,
                            TaskType = item.TaskType,
                            Description = item.Description,
                            CategoryId = item.CategoryId
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return TasksDTO;
            }
        }

        public static async Task<TaskDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                TasksRepository TasksRepo = new TasksRepository(unitOfWork);
                TaskDTO TaskDTO = new TaskDTO();

                Freelance_Data.Entities.TaskRelated.Task Task = await TasksRepo.GetById(id);

                if (Task != null)
                {
                    TaskDTO.Id = Task.Id;
                    TaskDTO.UserId = Task.UserId;
                    TaskDTO.DateOfPosting = Task.DateOfPosting;
                    TaskDTO.ProjectName = Task.ProjectName;
                    TaskDTO.Location = Task.Location;
                    TaskDTO.MinPrice = Task.MinPrice;
                    TaskDTO.MaxPrice = Task.MaxPrice;
                    TaskDTO.TaskType = Task.TaskType;
                    TaskDTO.Description = Task.Description;
                    TaskDTO.CategoryId = Task.CategoryId;


                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return TaskDTO;
            }
        }

        public static async System.Threading.Tasks.Task Save(TaskDTO TaskDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                TasksRepository TasksRepo = new TasksRepository(unitOfWork);
                Freelance_Data.Entities.TaskRelated.Task Task = new Freelance_Data.Entities.TaskRelated.Task();

                if (TaskDTO != null)
                {
                    if (TaskDTO.Id == 0)
                    {
                        Task = new Freelance_Data.Entities.TaskRelated.Task
                        {
                            UserId = TaskDTO.UserId,
                            DateOfPosting = TaskDTO.DateOfPosting,
                            ProjectName = TaskDTO.ProjectName,
                            Location = TaskDTO.Location,
                            MinPrice = TaskDTO.MinPrice,
                            MaxPrice = TaskDTO.MaxPrice,
                            TaskType = TaskDTO.TaskType,
                            Description = TaskDTO.Description,
                            CategoryId = TaskDTO.CategoryId
                        };
                    }
                    else
                    {
                        Task = new Freelance_Data.Entities.TaskRelated.Task
                        {
                            Id = TaskDTO.Id,
                            UserId = TaskDTO.UserId,
                            DateOfPosting = TaskDTO.DateOfPosting,
                            ProjectName = TaskDTO.ProjectName,
                            Location = TaskDTO.Location,
                            MinPrice = TaskDTO.MinPrice,
                            MaxPrice = TaskDTO.MaxPrice,
                            TaskType = TaskDTO.TaskType,
                            Description = TaskDTO.Description,
                            CategoryId = TaskDTO.CategoryId

                        };
                    }

                    await TasksRepo.Save(Task);
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

                TasksRepository TasksRepo = new TasksRepository(unitOfWork);
                FilesToTasksRepository FilesToTasksRepo = new FilesToTasksRepository(unitOfWork);
                SkillsToTasksRepository SkillsToTasksRepo = new SkillsToTasksRepository(unitOfWork);
                TaskBidsRepository taskBidsRepo = new TaskBidsRepository(unitOfWork);
                BookmarkedTasksRepository bookmarkedTasksRepo = new BookmarkedTasksRepository(unitOfWork);

                Freelance_Data.Entities.TaskRelated.Task Task = await TasksRepo.GetById(id);

                if (Task != null)
                {
                    List<BookmarkedTask> bookmarkedTask = await bookmarkedTasksRepo.GetAll(c => c.TaskId == id);
                    foreach (var task in bookmarkedTask)
                    {
                        await bookmarkedTasksRepo.Delete(task);
                    }

                    List<FileToTask> filesToTasks = await FilesToTasksRepo.GetAll(c => c.TaskId == id);
                    foreach (var item in filesToTasks)
                    {
                        await FilesToTasksRepo.Delete(item);
                    }

                    List<SkillToTask> skillsToTasks = await SkillsToTasksRepo.GetAll(c => c.TaskId == id);
                    foreach (var item in skillsToTasks)
                    {
                        await SkillsToTasksRepo.Delete(item);
                    }

                    List<TaskBid> taskBids = await taskBidsRepo.GetAll(c => c.TaskId == id);
                    foreach (var item in taskBids)
                    {
                        await taskBidsRepo.Delete(item);
                    }

                    await TasksRepo.Delete(Task);
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
