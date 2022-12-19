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
    public class JobApplicationsManagementService : IBaseManagementService
    {
        public static async Task<List<JobApplicationDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                JobApplicationsRepository JobApplicationsRepo = new JobApplicationsRepository(unitOfWork);
                List<JobApplication> JobApplications = await JobApplicationsRepo.GetAll();

                List<JobApplicationDTO> JobApplicationsDTO = new List<JobApplicationDTO>();

                if (JobApplications != null)
                {
                    foreach (var item in JobApplications)
                    {
                        JobApplicationsDTO.Add(new JobApplicationDTO
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
                return JobApplicationsDTO;
            }
        }

        public static async Task<JobApplicationDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                JobApplicationsRepository JobApplicationsRepo = new JobApplicationsRepository(unitOfWork);
                JobApplicationDTO JobApplicationDTO = new JobApplicationDTO();

                JobApplication JobApplication = await JobApplicationsRepo.GetById(id);

                if (JobApplication != null)
                {
                    JobApplicationDTO.Id = JobApplication.Id;
                    JobApplicationDTO.UserId = JobApplication.UserId;
                    JobApplicationDTO.JobId = JobApplication.JobId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return JobApplicationDTO;
            }
        }

        public static async Task Save(JobApplicationDTO JobApplicationDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                JobApplicationsRepository JobApplicationsRepo = new JobApplicationsRepository(unitOfWork);
                JobApplication JobApplication = new JobApplication();

                if (JobApplicationDTO != null)
                {
                    if (JobApplicationDTO.Id == 0)
                    {
                        JobApplication = new JobApplication
                        {
                            UserId = JobApplicationDTO.UserId,
                            JobId = JobApplicationDTO.JobId
                        };
                    }
                    else
                    {
                        JobApplication = new JobApplication
                        {
                            Id = JobApplicationDTO.Id,
                            UserId = JobApplicationDTO.UserId,
                            JobId = JobApplicationDTO.JobId
                        };
                    }

                    await JobApplicationsRepo.Save(JobApplication);
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

                JobApplicationsRepository JobApplicationsRepo = new JobApplicationsRepository(unitOfWork);
                JobApplication JobApplication = await JobApplicationsRepo.GetById(id);

                if (JobApplication != null)
                {
                    await JobApplicationsRepo.Delete(JobApplication);
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
