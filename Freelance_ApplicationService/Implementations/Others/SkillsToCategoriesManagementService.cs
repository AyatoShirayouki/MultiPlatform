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

namespace Freelance_ApplicationService.Implementations.Others
{
    public class SkillsToCategoriesManagementService : IBaseManagementService
    {
        public static async Task<List<SkillToCategoryDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                SkillsToCategoriesRepository SkillToCategorysRepo = new SkillsToCategoriesRepository(unitOfWork);
                List<SkillToCategory> SkillToCategorys = await SkillToCategorysRepo.GetAll();

                List<SkillToCategoryDTO> SkillToCategorysDTO = new List<SkillToCategoryDTO>();

                if (SkillToCategorys != null)
                {
                    foreach (var item in SkillToCategorys)
                    {
                        SkillToCategorysDTO.Add(new SkillToCategoryDTO
                        {
                            Id = item.Id,
                            CategoryId = item.CategoryId,
                            SkillId = item.SkillId
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return SkillToCategorysDTO;
            }
        }

        public static async Task<SkillToCategoryDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                SkillsToCategoriesRepository SkillToCategorysRepo = new SkillsToCategoriesRepository(unitOfWork);
                SkillToCategoryDTO SkillToCategoryDTO = new SkillToCategoryDTO();

                SkillToCategory SkillToCategory = await SkillToCategorysRepo.GetById(id);

                if (SkillToCategory != null)
                {
                    SkillToCategoryDTO.Id = SkillToCategory.Id;
                    SkillToCategoryDTO.CategoryId = SkillToCategory.CategoryId;
                    SkillToCategoryDTO.SkillId = SkillToCategory.SkillId;
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return SkillToCategoryDTO;
            }
        }

        public static async Task Save(SkillToCategoryDTO SkillToCategoryDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                SkillsToCategoriesRepository SkillToCategorysRepo = new SkillsToCategoriesRepository(unitOfWork);
                SkillToCategory SkillToCategory = new SkillToCategory();

                if (SkillToCategoryDTO != null)
                {
                    if (SkillToCategoryDTO.Id == 0)
                    {
                        SkillToCategory = new SkillToCategory
                        {
                            CategoryId = SkillToCategoryDTO.CategoryId,
                            SkillId = SkillToCategoryDTO.SkillId
                        };
                    }
                    else
                    {
                        SkillToCategory = new SkillToCategory
                        {
                            Id = SkillToCategoryDTO.Id,
                            CategoryId = SkillToCategoryDTO.CategoryId,
                            SkillId = SkillToCategoryDTO.SkillId
                        };
                    }

                    await SkillToCategorysRepo.Save(SkillToCategory);
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

                SkillsToCategoriesRepository SkillToCategorysRepo = new SkillsToCategoriesRepository(unitOfWork);
                SkillToCategory SkillToCategory = await SkillToCategorysRepo.GetById(id);

                if (SkillToCategory != null)
                {
                    await SkillToCategorysRepo.Delete(SkillToCategory);
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
