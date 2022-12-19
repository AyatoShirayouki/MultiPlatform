using Base.ManagementService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_ApplicationService.DTOs;
using Users_Data.Entities;
using Users_Repository.Implementations.EntityRepositories;
using Users_Repository.Implementations;

namespace Users_ApplicationService.Implementations
{
    public class PricingPlansManagementService : IBaseManagementService
    {
        public static async Task<List<PricingPlanDTO>> GetAll()
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                PricingPlansRepository PricingPlansRepo = new PricingPlansRepository(unitOfWork);
                List<PricingPlan> PricingPlans = await PricingPlansRepo.GetAll();

                List<PricingPlanDTO> PricingPlansDTO = new List<PricingPlanDTO>();

                if (PricingPlans != null)
                {
                    foreach (var item in PricingPlans)
                    {
                        PricingPlansDTO.Add(new PricingPlanDTO
                        {
                            Id = item.Id,
                            Title = item.Title,
                            Price = item.Price,
                            Description = item.Description
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return PricingPlansDTO;
            }
        }

        public static async Task<PricingPlanDTO> GetById(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                PricingPlansRepository PricingPlansRepo = new PricingPlansRepository(unitOfWork);
                PricingPlanDTO PricingPlanDTO = new PricingPlanDTO();

                PricingPlan PricingPlan = await PricingPlansRepo.GetById(id);

                if (PricingPlan != null)
                {
                    PricingPlanDTO.Id = PricingPlan.Id;
                    PricingPlanDTO.Title = PricingPlan.Title;
                    PricingPlanDTO.Price = PricingPlan.Price;
                    PricingPlanDTO.Description = PricingPlan.Description;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return PricingPlanDTO;
            }
        }

        public static async Task<PricingPlanDTO> GetPricingPlanByName(string name)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();
                PricingPlansRepository PricingPlansRepo = new PricingPlansRepository();
                PricingPlan PricingPlan = await PricingPlansRepo.GetFirstOrDefault(u => u.Title.Contains(name));
                PricingPlanDTO PricingPlanDTO = new PricingPlanDTO();

                if (PricingPlan != null)
                {
                    PricingPlanDTO = new PricingPlanDTO
                    {
                        Id = PricingPlan.Id,
                        Title = PricingPlan.Title,
                        Price = PricingPlan.Price,
                        Description = PricingPlan.Description
                    };

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return PricingPlanDTO;
            }
        }

        public static async Task Save(PricingPlanDTO PricingPlanDTO)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                PricingPlansRepository PricingPlansRepo = new PricingPlansRepository(unitOfWork);
                PricingPlan PricingPlan = new PricingPlan();

                if (PricingPlanDTO != null)
                {
                    if (PricingPlanDTO.Id == 0)
                    {
                        PricingPlan = new PricingPlan
                        {
                            Title = PricingPlanDTO.Title,
                            Price = PricingPlanDTO.Price,
                            Description = PricingPlanDTO.Description
                        };
                    }
                    else
                    {
                        PricingPlan = new PricingPlan
                        {
                            Id = PricingPlanDTO.Id,
                            Title = PricingPlanDTO.Title,
                            Price = PricingPlanDTO.Price,
                            Description = PricingPlanDTO.Description
                        };
                    }

                    await PricingPlansRepo.Save(PricingPlan);
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
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                PricingPlansRepository PricingPlansRepo = new PricingPlansRepository(unitOfWork);
                PricingPlanFeaturesRepository pricingPlanFeaturesRepo = new PricingPlanFeaturesRepository(unitOfWork);

                PricingPlan PricingPlan = await PricingPlansRepo.GetById(id);

                if (PricingPlan != null)
                {
                    List<PricingPlanFeature> pricingPlanFeatures = await pricingPlanFeaturesRepo.GetAll(a => a.PricingPlanId == PricingPlan.Id);
                    foreach (var feature in pricingPlanFeatures)
                    {
                        await pricingPlanFeaturesRepo.Delete(feature);
                    }

                    await PricingPlansRepo.Delete(PricingPlan);

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
