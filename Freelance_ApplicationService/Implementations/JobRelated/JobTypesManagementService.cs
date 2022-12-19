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
using Freelance_Repository.Implementations.EntityRepositories.JobRelated;
using Freelance_Data.Entities.JobRelated;
using Freelance_ApplicationService.DTOs.JobRelated;

namespace Freelance_ApplicationService.Implementations.JobRelated
{
    public class JobTypesManagementService : IBaseManagementService
    {
        public static async Task<List<JobTypeDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                JobTypesRepository JobTypesRepo = new JobTypesRepository(unitOfWork);
                List<JobType> JobTypes = await JobTypesRepo.GetAll();

                List<JobTypeDTO> JobTypesDTO = new List<JobTypeDTO>();

                if (JobTypes != null)
                {
                    foreach (var item in JobTypes)
                    {
                        JobTypesDTO.Add(new JobTypeDTO
                        {
                            Id = item.Id,
                            Name = item.Name
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return JobTypesDTO;
            }
        }

        public static async Task<JobTypeDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                JobTypesRepository JobTypesRepo = new JobTypesRepository(unitOfWork);
                JobTypeDTO JobTypeDTO = new JobTypeDTO();

                JobType JobType = await JobTypesRepo.GetById(id);

                if (JobType != null)
                {
                    JobTypeDTO.Id = JobType.Id;
                    JobTypeDTO.Name = JobType.Name;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return JobTypeDTO;
            }
        }

        public static async Task Save(JobTypeDTO JobTypeDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                JobTypesRepository JobTypesRepo = new JobTypesRepository(unitOfWork);
                JobType JobType = new JobType();

                if (JobTypeDTO != null)
                {
                    if (JobTypeDTO.Id == 0)
                    {
                        JobType = new JobType
                        {
                            Name = JobTypeDTO.Name
                        };
                    }
                    else
                    {
                        JobType = new JobType
                        {
                            Id = JobTypeDTO.Id,
                            Name = JobTypeDTO.Name
                        };
                    }

                    await JobTypesRepo.Save(JobType);
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

                JobTypesRepository JobTypesRepo = new JobTypesRepository(unitOfWork);
                JobType JobType = await JobTypesRepo.GetById(id);

                if (JobType != null)
                {
                    await JobTypesRepo.Delete(JobType);
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
