using Base.ManagementService;
using Freelance_ApplicationService.DTOs.JobRelated;
using Freelance_Data.Entities.JobRelated;
using Freelance_Repository.Implementations.EntityRepositories.JobRelated;
using Freelance_Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.Implementations.JobRelated
{
    public class JobsManagementService : IBaseManagementService
    {
        public static async Task<List<JobDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                JobsRepository JobsRepo = new JobsRepository(unitOfWork);
                List<Job> Jobs = await JobsRepo.GetAll();

                List<JobDTO> JobsDTO = new List<JobDTO>();

                if (Jobs != null)
                {
                    foreach (var item in Jobs)
                    {
                        JobsDTO.Add(new JobDTO
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            DateOfPosting = item.DateOfPosting,
                            Title = item.Title,
                            Location = item.Location,
                            MinSalary = item.MinSalary,
                            MaxSalary = item.MaxSalary,
                            Description = item.Description,
                            CategoryId = item.CategoryId,
                            JobTypeId = item.JobTypeId
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return JobsDTO;
            }
        }

        public static async Task<JobDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                JobsRepository JobsRepo = new JobsRepository(unitOfWork);
                JobDTO JobDTO = new JobDTO();

                Job Job = await JobsRepo.GetById(id);

                if (Job != null)
                {
                    JobDTO.Id = Job.Id;
                    JobDTO.UserId = Job.UserId;
                    JobDTO.DateOfPosting = Job.DateOfPosting;
                    JobDTO.Title = Job.Title;
                    JobDTO.Location = Job.Location;
                    JobDTO.MinSalary = Job.MinSalary;
                    JobDTO.MaxSalary = Job.MaxSalary;
                    JobDTO.Description = Job.Description;
                    JobDTO.CategoryId = Job.CategoryId;
                    Job.JobTypeId = Job.JobTypeId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return JobDTO;
            }
        }

        public static async Task Save(JobDTO JobDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                JobsRepository JobsRepo = new JobsRepository(unitOfWork);
                Job Job = new Job();

                if (JobDTO != null)
                {
                    if (JobDTO.Id == 0)
                    {
                        Job = new Job
                        {
                            UserId = JobDTO.UserId,
                            DateOfPosting = JobDTO.DateOfPosting,
                            Title = JobDTO.Title,
                            Location = JobDTO.Location,
                            MinSalary = JobDTO.MinSalary,
                            MaxSalary = JobDTO.MaxSalary,
                            Description = JobDTO.Description,
                            CategoryId = JobDTO.CategoryId,
                            JobTypeId = JobDTO.JobTypeId
                        };
                    }
                    else
                    {
                        Job = new Job
                        {
                            Id = JobDTO.Id,
                            DateOfPosting = JobDTO.DateOfPosting,
                            Title = JobDTO.Title,
                            Location = JobDTO.Location,
                            MinSalary = JobDTO.MinSalary,
                            MaxSalary = JobDTO.MaxSalary,
                            Description = JobDTO.Description,
                            CategoryId = JobDTO.CategoryId,
                            JobTypeId = JobDTO.JobTypeId
                        };
                    }

                    await JobsRepo.Save(Job);
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

                JobsRepository JobsRepo = new JobsRepository(unitOfWork);
                JobApplicationsRepository jobApplicationsRepo = new JobApplicationsRepository(unitOfWork);
                SkillsToJobsRepository skillsToJobsRepo = new SkillsToJobsRepository(unitOfWork);
                TagsToJobsRepository tagsToJobsRepo = new TagsToJobsRepository(unitOfWork);
                FilesToJobsRepository filesToJobsRepo = new FilesToJobsRepository(unitOfWork);

                Job Job = await JobsRepo.GetById(id);

                if (Job != null)
                {
                    List<JobApplication> jobApplications = await jobApplicationsRepo.GetAll(c => c.JobId == Job.Id);
                    foreach (var jobApplication in jobApplications)
                    {
                        await jobApplicationsRepo.Delete(jobApplication);
                    }

                    List<SkillToJob> skillsToJobs = await skillsToJobsRepo.GetAll(c => c.JobId == Job.Id);
                    foreach (var skillToJob in skillsToJobs)
                    {
                        await skillsToJobsRepo.Delete(skillToJob);
                    }

                    List<TagToJob> tagsToJobs = await tagsToJobsRepo.GetAll(c => c.JobId == Job.Id);
                    foreach (var tagToJob in tagsToJobs)
                    {
                        await tagsToJobsRepo.Delete(tagToJob);
                    }

                    List<FileToJob> filesToJobs = await filesToJobsRepo.GetAll(c => c.JobId == Job.Id);
                    foreach (var fileToJob in filesToJobs)
                    {
                        await filesToJobsRepo.Delete(fileToJob);
                    }

                    await JobsRepo.Delete(Job);
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
