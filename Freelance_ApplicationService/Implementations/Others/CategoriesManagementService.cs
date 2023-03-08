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
using Freelance_ApplicationService.DTOs.Others;
using Freelance_Repository.Implementations.EntityRepositories.Others;
using Freelance_Data.Entities.Others;
using Freelance_Repository.Implementations.EntityRepositories.JobRelated;
using Freelance_Data.Entities.JobRelated;

namespace Freelance_ApplicationService.Implementations.Others
{
    public class CategoriesManagementService : IBaseManagementService
    {
        public static async Task<List<CategoryDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                CategoriesRepository CategoriesRepo = new CategoriesRepository(unitOfWork);
                List<Category> Categories = await CategoriesRepo.GetAll();

                List<CategoryDTO> CategoriesDTO = new List<CategoryDTO>();

                if (Categories != null)
                {
                    foreach (var item in Categories)
                    {
                        CategoriesDTO.Add(new CategoryDTO
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item.Description,
                            CategoryIcon = item.CategoryIcon,
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return CategoriesDTO;
            }
        }

        public static async Task<CategoryDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                CategoriesRepository CategoriesRepo = new CategoriesRepository(unitOfWork);
                CategoryDTO CategoryDTO = new CategoryDTO();

                Category Category = await CategoriesRepo.GetById(id);

                if (Category != null)
                {
                    CategoryDTO.Id = Category.Id;
                    CategoryDTO.Name = Category.Name;
                    CategoryDTO.Description = Category.Description;
                    CategoryDTO.CategoryIcon = Category.CategoryIcon;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return CategoryDTO;
            }
        }

        public static async Task Save(CategoryDTO CategoryDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                CategoriesRepository CategoriesRepo = new CategoriesRepository(unitOfWork);
                Category Category = new Category();

                if (CategoryDTO != null)
                {
                    if (CategoryDTO.Id == 0)
                    {
                        Category = new Category
                        {
                            Name = CategoryDTO.Name,
                            Description = CategoryDTO.Description,
                            CategoryIcon = CategoryDTO.CategoryIcon
                        };
                    }
                    else
                    {
                        Category = new Category
                        {
                            Id = CategoryDTO.Id,
                            Name = CategoryDTO.Name, 
                            Description = CategoryDTO.Description, 
                            CategoryIcon = CategoryDTO.CategoryIcon
                        };
                    }

                    await CategoriesRepo.Save(Category);
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

                CategoriesRepository CategoriesRepo = new CategoriesRepository(unitOfWork);

                JobsRepository JobsRepo = new JobsRepository(unitOfWork);
                JobApplicationsRepository jobApplicationsRepo = new JobApplicationsRepository(unitOfWork);
                SkillsToJobsRepository skillsToJobsRepo = new SkillsToJobsRepository(unitOfWork);
                TagsToJobsRepository tagsToJobsRepo = new TagsToJobsRepository(unitOfWork);
                FilesToJobsRepository filesToJobsRepo = new FilesToJobsRepository(unitOfWork);

                Category Category = await CategoriesRepo.GetById(id);

                if (Category != null)
                {
                    List<Job> jobs = await JobsRepo.GetAll(c => c.CategoryId == id);
                    foreach (var Job in jobs)
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
                    }

                    await CategoriesRepo.Delete(Category);
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
