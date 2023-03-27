using Base.ManagementService;
using Users_ApplicationService.DTOs;
using Users_Data.Entities.AddressInfo;
using Users_Data.Entities.Education;
using Users_Data.Entities;
using Users_Repository.Implementations.EntityRepositories.AddressInfo;
using Users_Repository.Implementations.EntityRepositories.Education;
using Users_Repository.Implementations.EntityRepositories;
using Users_Repository.Implementations;
using Freelance_Repository.Implementations.EntityRepositories.JobRelated;
using Freelance_Repository.Implementations;
using Freelance_Repository.Implementations.EntityRepositories.Bookmarks;
using Freelance_Repository.Implementations.EntityRepositories.Others;
using Freelance_Repository.Implementations.EntityRepositories.TaskRelated;
using Freelance_Data.Entities.Bookmarks;
using Freelance_Data.Entities.JobRelated;
using Freelance_Data.Entities.Others;
using Freelance_Data.Entities.TaskRelated;

namespace Users_ApplicationService.Implementations
{
    public class UsersManagementService : IBaseManagementService
    {
        public static async Task<List<UserDTO>> GetAll()
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                UsersRepository usersRepo = new UsersRepository(unitOfWork);
                List<User> users = await usersRepo.GetAll();

                List<UserDTO> usersDTO = new List<UserDTO>();

                if (users != null)
                {
                    foreach (var item in users)
                    {
                        usersDTO.Add(new UserDTO
                        {
                            Id = item.Id,
                            Email = item.Email,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Password = item.Password,
                            Gender = item.Gender,
                            DOB = item.DOB,
                            PhoneNumber = item.PhoneNumber,
                            Description = item.Description,
                            LinkedInAccount = item.LinkedInAccount,
                            AddressId = item.AddressId,
                            PricingPlanId = item.PricingPlanId,
                            IsCompany = item.IsCompany,
                            CompanyName = item.CompanyName
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return usersDTO;
            }
        }

        public static async Task<UserDTO> GetById(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                UsersRepository usersRepo = new UsersRepository(unitOfWork);
                UserDTO userDTO = new UserDTO();

                User user = await usersRepo.GetById(id);

                if (user != null)
                {
                    userDTO.Id = user.Id;
                    userDTO.Email = user.Email;
                    userDTO.Password = user.Password;
                    userDTO.FirstName = user.FirstName;
                    userDTO.LastName = user.LastName;
                    userDTO.Gender = user.Gender;
                    userDTO.DOB = user.DOB;
                    userDTO.Description = user.Description;
                    userDTO.LinkedInAccount = user.LinkedInAccount;
                    userDTO.PhoneNumber = user.PhoneNumber;
                    userDTO.AddressId = user.AddressId;
                    userDTO.PricingPlanId = user.PricingPlanId;
                    userDTO.IsCompany = user.IsCompany;
                    userDTO.CompanyName = user.CompanyName;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return userDTO;
            }
        }

        public static async System.Threading.Tasks.Task Save(UserDTO userDTO)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                UsersRepository usersRepo = new UsersRepository(unitOfWork);
                User user = new User();

                if (userDTO != null)
                {
                    AddressesRepository addressesRepo = new AddressesRepository(unitOfWork);
                    Address address = await addressesRepo.GetById(userDTO.AddressId);

                    if (address != null)
                    {
                        if (userDTO.Id == 0)
                        {
                            user = new User
                            {
                                Email = userDTO.Email,
                                FirstName = userDTO.FirstName,
                                LastName = userDTO.LastName,
                                Password = userDTO.Password,
                                Gender = userDTO.Gender,
                                DOB = userDTO.DOB,
                                PhoneNumber = userDTO.PhoneNumber,
                                Description = userDTO.Description,
                                LinkedInAccount = userDTO.LinkedInAccount,
                                AddressId = userDTO.AddressId,
                                PricingPlanId = userDTO.PricingPlanId,
                                IsCompany = userDTO.IsCompany,
                                CompanyName = userDTO.CompanyName
                            };
                        }
                        else
                        {
                            user = new User
                            {
                                Id = userDTO.Id,
                                Email = userDTO.Email,
                                FirstName = userDTO.FirstName,
                                LastName = userDTO.LastName,
                                Password = userDTO.Password,
                                Gender = userDTO.Gender,
                                DOB = userDTO.DOB,
                                PhoneNumber = userDTO.PhoneNumber,
                                Description = userDTO.Description,
                                LinkedInAccount = userDTO.LinkedInAccount,
                                AddressId = userDTO.AddressId,
                                PricingPlanId = userDTO.PricingPlanId,
                                IsCompany = userDTO.IsCompany,
                                CompanyName = userDTO.CompanyName
                            };
                        }

						await usersRepo.Save(user);

						unitOfWork.Commit();
					}
				}
                else
                {
                    unitOfWork.Rollback();
                }
            }
        }

        public static async Task<bool> VerifyEmail(string email)
        {
            bool exists = true;

            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();
                UsersRepository usersRepo = new UsersRepository();
                User user = await usersRepo.GetFirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    exists = false;
                }

                unitOfWork.Commit();

                return exists;
            }
        }

        public static async System.Threading.Tasks.Task Delete(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                UsersRepository usersRepo = new UsersRepository(unitOfWork);
                AddressesRepository addressesRepo = new AddressesRepository(unitOfWork);
                RefreshUserTokensRepository refreshUserTokensRepo = new RefreshUserTokensRepository(unitOfWork);
                UserEducationsRepository usersEducationRepo = new UserEducationsRepository(unitOfWork);
                UserFilesRepository userFilesRepo = new UserFilesRepository(unitOfWork);

                User user = await usersRepo.GetById(id);

                if (user != null)
                {
                    await DeleteUserRelatedData(user.Id);

                    Address address = await addressesRepo.GetById(user.AddressId);

                    List<UserEducation> userEducations = await usersEducationRepo.GetAll(i => i.UserId == user.Id);
                    foreach (UserEducation education in userEducations)
                    {
                        await usersEducationRepo.Delete(education);
                    }

                    List<UserFile> userImages = await userFilesRepo.GetAll(i => i.UserId == user.Id);
                    foreach (UserFile image in userImages)
                    {
                        await userFilesRepo.Delete(image);
                    }

                    List<RefreshUserToken> refreshUserTokens = await refreshUserTokensRepo.GetAll(t => t.UserId == user.Id);
                    foreach (var token in refreshUserTokens)
                    {
                        await refreshUserTokensRepo.Delete(token);
                    }

                    await usersRepo.Delete(user);
                    await addressesRepo.Delete(address);

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
            }
        }

        private static async System.Threading.Tasks.Task DeleteUserRelatedData(int userId)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                BookmarkedJobsRepository bookmarkedJobsRepo = new BookmarkedJobsRepository(unitOfWork);
                BookmarkedTasksRepository bookmarkedTasksRepo = new BookmarkedTasksRepository(unitOfWork);
                BookmarkedUsersRepository bookmarkedUsersRepo = new BookmarkedUsersRepository(unitOfWork);

                FilesToJobsRepository filesToJobsRepo = new FilesToJobsRepository(unitOfWork);
                JobsRepository jobsRepo = new JobsRepository(unitOfWork);
                JobApplicationsRepository jobApplicationsRepo = new JobApplicationsRepository(unitOfWork);
                SkillsToJobsRepository skillsToJobsRepo = new SkillsToJobsRepository(unitOfWork);
                TagsToJobsRepository tagsToJobsRepo = new TagsToJobsRepository(unitOfWork);

                NotesRepository notesRepo = new NotesRepository(unitOfWork);
                ReviewsRepository reviewsRepo = new ReviewsRepository(unitOfWork);

                FilesToTasksRepository filesToTasksRepo = new FilesToTasksRepository(unitOfWork);
                SkillsToTasksRepository skillsToTasksRepo = new SkillsToTasksRepository(unitOfWork);
                TasksRepository tasksRepo = new TasksRepository(unitOfWork);
                TaskBidsRepository taskBidsRepo = new TaskBidsRepository(unitOfWork);

                //delete bookmarks
                List<BookmarkedJob> bookmarkedJobs = await bookmarkedJobsRepo.GetAll(c => c.UserId == userId);
                foreach (var item in bookmarkedJobs)
                {
                    await bookmarkedJobsRepo.Delete(item);
                }

                List<BookmarkedTask> bookmarkedTasks = await bookmarkedTasksRepo.GetAll(c => c.UserId == userId);
                foreach (var item in bookmarkedTasks)
                {
                    await bookmarkedTasksRepo.Delete(item);
                }

                List<BookmarkedUser> bookmarkedUsers = await bookmarkedUsersRepo.GetAll(c => c.UserId == userId);
                foreach (var item in bookmarkedUsers)
                {
                    await bookmarkedUsersRepo.Delete(item);
                }

                //delete job related
                List<Job> jobs = await jobsRepo.GetAll(c => c.UserId == userId);
                foreach (var item in jobs)
                {
                    List<FileToJob> filesToJob = await filesToJobsRepo.GetAll(c => c.JobId == item.Id);
                    foreach (var fileToJob in filesToJob)
                    {
                        await filesToJobsRepo.Delete(fileToJob);
                    }

                    List<JobApplication> jobApplications = await jobApplicationsRepo.GetAll(c => c.JobId == item.Id);
                    foreach (var jobApllication in jobApplications)
                    {
                        await jobApplicationsRepo.Delete(jobApllication);
                    }

                    List<SkillToJob> skillsToJobs = await skillsToJobsRepo.GetAll(c => c.JobId == item.Id);
                    foreach (var skillToJob in skillsToJobs)
                    {
                        await skillsToJobsRepo.Delete(skillToJob);
                    }

                    List<TagToJob> tagsToJobs = await tagsToJobsRepo.GetAll(c => c.JobId == item.Id);
                    foreach (var tagToJob in tagsToJobs)
                    {
                        await tagsToJobsRepo.Delete(tagToJob);
                    }

                    await jobsRepo.Delete(item);
                }

                //task related
                List<Freelance_Data.Entities.TaskRelated.Task> tasks = await tasksRepo.GetAll(c => c.UserId == userId);
                foreach (var item in tasks)
                {
                    List<Review> reviews = await reviewsRepo.GetAll(c => c.UserId == userId && c.TaskId == item.Id);
                    foreach (var review in reviews)
                    {
                        await reviewsRepo.Delete(review);
                    }

                    List<FileToTask> filesToTasks = await filesToTasksRepo.GetAll(c => c.TaskId == item.Id);
                    foreach (var fileToTask in filesToTasks)
                    {
                        await filesToTasksRepo.Delete(fileToTask);
                    }

                    List<SkillToTask> skillsToTasks = await skillsToTasksRepo.GetAll(c => c.TaskId == item.Id);
                    foreach (var dkillToTask in skillsToTasks)
                    {
                        await skillsToTasksRepo.Delete(dkillToTask);
                    }

                    List<TaskBid> taskBids = await taskBidsRepo.GetAll(c => c.TaskId == item.Id);
                    foreach (var taskBid in taskBids)
                    {
                        await taskBidsRepo.Delete(taskBid);
                    }

                    await tasksRepo.Delete(item);
                }

                List<Note> notes = await notesRepo.GetAll(c => c.UserId == userId);
                foreach (var note in notes)
                {
                    await notesRepo.Delete(note);
                }
            }
        }
    }
}
