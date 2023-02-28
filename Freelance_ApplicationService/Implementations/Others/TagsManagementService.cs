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
using Freelance_Repository.Implementations.EntityRepositories.JobRelated;
using Freelance_Data.Entities.JobRelated;
using Freelance_ApplicationService.DTOs.TaskRelated;

namespace Freelance_ApplicationService.Implementations.Others
{
    public class TagsManagementService : IBaseManagementService
    {
        public static async Task<List<TagDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                TagsRepository TagsRepo = new TagsRepository(unitOfWork);
                List<Tag> Tags = await TagsRepo.GetAll();

                List<TagDTO> TagsDTO = new List<TagDTO>();

                if (Tags != null)
                {
                    foreach (var item in Tags)
                    {
                        TagsDTO.Add(new TagDTO
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
                return TagsDTO;
            }
        }

        public static async Task<TagDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                TagsRepository TagsRepo = new TagsRepository(unitOfWork);
                TagDTO TagDTO = new TagDTO();

                Tag Tag = await TagsRepo.GetById(id);

                if (Tag != null)
                {
                    TagDTO.Id = Tag.Id;
                    TagDTO.Name = Tag.Name;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return TagDTO;
            }
        }

        public static async Task Save(TagDTO TagDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                TagsRepository TagsRepo = new TagsRepository(unitOfWork);
                Tag Tag = new Tag();

                if (TagDTO != null)
                {
                    if (TagDTO.Id == 0)
                    {
                        Tag = new Tag
                        {
                            Name = TagDTO.Name
                        };
                    }
                    else
                    {
                        Tag = new Tag
                        {
                            Id = TagDTO.Id,
                            Name = TagDTO.Name
                        };
                    }

                    await TagsRepo.Save(Tag);
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

                TagsRepository TagsRepo = new TagsRepository(unitOfWork);
                TagsToJobsRepository tagsToJobsRepo = new TagsToJobsRepository(unitOfWork);

                Tag Tag = await TagsRepo.GetById(id);

                if (Tag != null)
                {
                    List<TagToJob> tagsTojobs = await tagsToJobsRepo.GetAll(c => c.TagId == id);
                    foreach (var item in tagsTojobs)
                    {
                        await tagsToJobsRepo.Delete(item);
                    }

                    await TagsRepo.Delete(Tag);
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
