using Base.ManagementService;
using Freelance_ApplicationService.DTOs.Others;
using Freelance_Data.Entities.Others;
using Freelance_Repository.Implementations.EntityRepositories.Others;
using Freelance_Repository.Implementations;
using Freelance_Repository.Implementations.EntityRepositories.JobRelated;
using Freelance_Data.Entities.JobRelated;

namespace Freelance_ApplicationService.Implementations.Others
{
    public class SkillsManagementService : IBaseManagementService
    {
        public static async Task<List<SkillDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                SkillsRepository SkillsRepo = new SkillsRepository(unitOfWork);
                List<Skill> Skills = await SkillsRepo.GetAll();

                List<SkillDTO> SkillsDTO = new List<SkillDTO>();

                if (Skills != null)
                {
                    foreach (var item in Skills)
                    {
                        SkillsDTO.Add(new SkillDTO
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
                return SkillsDTO;
            }
        }

        public static async Task<SkillDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                SkillsRepository SkillsRepo = new SkillsRepository(unitOfWork);
                SkillDTO SkillDTO = new SkillDTO();

                Skill Skill = await SkillsRepo.GetById(id);

                if (Skill != null)
                {
                    SkillDTO.Id = Skill.Id;
                    SkillDTO.Name = Skill.Name;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return SkillDTO;
            }
        }

        public static async Task Save(SkillDTO SkillDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                SkillsRepository SkillsRepo = new SkillsRepository(unitOfWork);
                Skill Skill = new Skill();

                if (SkillDTO != null)
                {
                    if (SkillDTO.Id == 0)
                    {
                        Skill = new Skill
                        {
                            Name = SkillDTO.Name
                        };
                    }
                    else
                    {
                        Skill = new Skill
                        {
                            Id = SkillDTO.Id,
                            Name = SkillDTO.Name
                        };
                    }

                    await SkillsRepo.Save(Skill);
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

                SkillsRepository SkillsRepo = new SkillsRepository(unitOfWork);
                SkillsToCategoriesRepository skillsToCategoriesRepo = new SkillsToCategoriesRepository(unitOfWork);
                SkillsToJobsRepository skillsToJobsRepo = new SkillsToJobsRepository(unitOfWork);

                Skill Skill = await SkillsRepo.GetById(id);

                if (Skill != null)
                {
                    List<SkillToCategory> skillsTocategories = await skillsToCategoriesRepo.GetAll(c => c.SkillId == id);
                    foreach (SkillToCategory skillToCategory in skillsTocategories)
                    {
                        await skillsToCategoriesRepo.Delete(skillToCategory);
                    }

                    List<SkillToJob> skillsToJobs = await skillsToJobsRepo.GetAll(c => c.SkillId == id);
                    foreach (var item in skillsToJobs)
                    {
                        await skillsToJobsRepo.Delete(item);
                    }

                    await SkillsRepo.Delete(Skill);
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
