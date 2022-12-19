using Base.ManagementService;
using Freelance_Repository.Implementations;
using Freelance_ApplicationService.DTOs.JobRelated;
using Freelance_Data.Entities.JobRelated;
using Freelance_Repository.Implementations.EntityRepositories.JobRelated;

namespace Freelance_ApplicationService.Implementations.JobRelated
{
    public class SkillsToJobsManagementService : IBaseManagementService
    {
        public static async Task<List<SkillToJobDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                SkillsToJobsRepository SkillToJobsRepo = new SkillsToJobsRepository(unitOfWork);
                List<SkillToJob> SkillToJobs = await SkillToJobsRepo.GetAll();

                List<SkillToJobDTO> SkillToJobsDTO = new List<SkillToJobDTO>();

                if (SkillToJobs != null)
                {
                    foreach (var item in SkillToJobs)
                    {
                        SkillToJobsDTO.Add(new SkillToJobDTO
                        {
                            Id = item.Id,
                            SkillId = item.SkillId,
                            JobId = item.JobId
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return SkillToJobsDTO;
            }
        }

        public static async Task<SkillToJobDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                SkillsToJobsRepository SkillToJobsRepo = new SkillsToJobsRepository(unitOfWork);
                SkillToJobDTO SkillToJobDTO = new SkillToJobDTO();

                SkillToJob SkillToJob = await SkillToJobsRepo.GetById(id);

                if (SkillToJob != null)
                {
                    SkillToJobDTO.Id = SkillToJob.Id;
                    SkillToJobDTO.SkillId = SkillToJob.SkillId;
                    SkillToJobDTO.JobId = SkillToJob.JobId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return SkillToJobDTO;
            }
        }

        public static async Task Save(SkillToJobDTO SkillToJobDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                SkillsToJobsRepository SkillToJobsRepo = new SkillsToJobsRepository(unitOfWork);
                SkillToJob SkillToJob = new SkillToJob();

                if (SkillToJobDTO != null)
                {
                    if (SkillToJobDTO.Id == 0)
                    {
                        SkillToJob = new SkillToJob
                        {
                            SkillId = SkillToJobDTO.SkillId,
                            JobId = SkillToJobDTO.JobId
                        };
                    }
                    else
                    {
                        SkillToJob = new SkillToJob
                        {
                            Id = SkillToJobDTO.Id,
                            SkillId = SkillToJobDTO.SkillId,
                            JobId = SkillToJobDTO.JobId
                        };
                    }

                    await SkillToJobsRepo.Save(SkillToJob);
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

                SkillsToJobsRepository SkillToJobsRepo = new SkillsToJobsRepository(unitOfWork);
                SkillToJob SkillToJob = await SkillToJobsRepo.GetById(id);

                if (SkillToJob != null)
                {
                    await SkillToJobsRepo.Delete(SkillToJob);
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
