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
    public class TagsToJobsManagementService : IBaseManagementService
    {
        public static async Task<List<TagToJobDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                TagsToJobsRepository TagToJobsRepo = new TagsToJobsRepository(unitOfWork);
                List<TagToJob> TagToJobs = await TagToJobsRepo.GetAll();

                List<TagToJobDTO> TagToJobsDTO = new List<TagToJobDTO>();

                if (TagToJobs != null)
                {
                    foreach (var item in TagToJobs)
                    {
                        TagToJobsDTO.Add(new TagToJobDTO
                        {
                            Id = item.Id,
                            TagId = item.TagId,
                            JobId = item.JobId
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return TagToJobsDTO;
            }
        }

        public static async Task<TagToJobDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                TagsToJobsRepository TagToJobsRepo = new TagsToJobsRepository(unitOfWork);
                TagToJobDTO TagToJobDTO = new TagToJobDTO();

                TagToJob TagToJob = await TagToJobsRepo.GetById(id);

                if (TagToJob != null)
                {
                    TagToJobDTO.Id = TagToJob.Id;
                    TagToJobDTO.TagId = TagToJob.TagId;
                    TagToJobDTO.JobId = TagToJob.JobId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return TagToJobDTO;
            }
        }

        public static async Task Save(TagToJobDTO TagToJobDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                TagsToJobsRepository TagToJobsRepo = new TagsToJobsRepository(unitOfWork);
                TagToJob TagToJob = new TagToJob();

                if (TagToJobDTO != null)
                {
                    if (TagToJobDTO.Id == 0)
                    {
                        TagToJob = new TagToJob
                        {
                            TagId = TagToJobDTO.TagId,
                            JobId = TagToJobDTO.JobId
                        };
                    }
                    else
                    {
                        TagToJob = new TagToJob
                        {
                            Id = TagToJobDTO.Id,
                            TagId = TagToJobDTO.TagId,
                            JobId = TagToJobDTO.JobId
                        };
                    }

                    await TagToJobsRepo.Save(TagToJob);
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

                TagsToJobsRepository TagToJobsRepo = new TagsToJobsRepository(unitOfWork);
                TagToJob TagToJob = await TagToJobsRepo.GetById(id);

                if (TagToJob != null)
                {
                    await TagToJobsRepo.Delete(TagToJob);
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
