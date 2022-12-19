using Base.ManagementService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_ApplicationService.DTOs.AddressInfo;
using Users_Data.Entities.AddressInfo;
using Users_Repository.Implementations.EntityRepositories.AddressInfo;
using Users_Repository.Implementations;
using Users_ApplicationService.DTOs;
using Users_Data.Entities;
using Users_Repository.Implementations.EntityRepositories;

namespace Users_ApplicationService.Implementations
{
    public class PricingPlanFeaturesManagementService : IBaseManagementService
    {
        public static async Task<List<PricingPlanFeatureDTO>> GetAll()
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                PricingPlanFeaturesRepository PricingPlanFeaturesRepo = new PricingPlanFeaturesRepository(unitOfWork);
                List<PricingPlanFeature> PricingPlanFeatures = await PricingPlanFeaturesRepo.GetAll();

                List<PricingPlanFeatureDTO> PricingPlanFeaturesDTO = new List<PricingPlanFeatureDTO>();

                if (PricingPlanFeatures != null)
                {
                    foreach (var item in PricingPlanFeatures)
                    {
                        PricingPlanFeaturesDTO.Add(new PricingPlanFeatureDTO
                        {
                            Id = item.Id,
                            Name = item.Name,
                            PricingPlanId = item.PricingPlanId
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return PricingPlanFeaturesDTO;
            }
        }

        public static async Task<PricingPlanFeatureDTO> GetById(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                PricingPlanFeaturesRepository PricingPlanFeaturesRepo = new PricingPlanFeaturesRepository(unitOfWork);
                PricingPlanFeatureDTO PricingPlanFeatureDTO = new PricingPlanFeatureDTO();

                PricingPlanFeature PricingPlanFeature = await PricingPlanFeaturesRepo.GetById(id);

                if (PricingPlanFeature != null)
                {
                    PricingPlanFeatureDTO.Id = PricingPlanFeature.Id;
                    PricingPlanFeatureDTO.Name = PricingPlanFeature.Name;
                    PricingPlanFeatureDTO.PricingPlanId = PricingPlanFeature.PricingPlanId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return PricingPlanFeatureDTO;
            }
        }

        public static async Task<PricingPlanFeatureDTO> GetPricingPlanFeatureByName(string name)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();
                PricingPlanFeaturesRepository PricingPlanFeaturesRepo = new PricingPlanFeaturesRepository();
                PricingPlanFeature PricingPlanFeature = await PricingPlanFeaturesRepo.GetFirstOrDefault(u => u.Name.Contains(name));
                PricingPlanFeatureDTO PricingPlanFeatureDTO = new PricingPlanFeatureDTO();

                if (PricingPlanFeature != null)
                {
                    PricingPlanFeatureDTO = new PricingPlanFeatureDTO
                    {
                        Id = PricingPlanFeature.Id,
                        Name = PricingPlanFeature.Name,
                        PricingPlanId = PricingPlanFeature.PricingPlanId
                    };

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return PricingPlanFeatureDTO;
            }
        }

        public static async Task Save(PricingPlanFeatureDTO PricingPlanFeatureDTO)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                PricingPlanFeaturesRepository PricingPlanFeaturesRepo = new PricingPlanFeaturesRepository(unitOfWork);
                PricingPlanFeature PricingPlanFeature = new PricingPlanFeature();

                if (PricingPlanFeatureDTO != null)
                {
                    if (PricingPlanFeatureDTO.Id == 0)
                    {
                        PricingPlanFeature = new PricingPlanFeature
                        {
                            Name = PricingPlanFeatureDTO.Name,
                            PricingPlanId = PricingPlanFeatureDTO.PricingPlanId
                        };
                    }
                    else
                    {
                        PricingPlanFeature = new PricingPlanFeature
                        {
                            Id = PricingPlanFeatureDTO.Id,
                            Name = PricingPlanFeatureDTO.Name,
                            PricingPlanId = PricingPlanFeatureDTO.PricingPlanId
                        };
                    }

                    await PricingPlanFeaturesRepo.Save(PricingPlanFeature);
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

                PricingPlanFeaturesRepository PricingPlanFeaturesRepo = new PricingPlanFeaturesRepository(unitOfWork);

                PricingPlanFeature PricingPlanFeature = await PricingPlanFeaturesRepo.GetById(id);

                if (PricingPlanFeature != null)
                {
                    await PricingPlanFeaturesRepo.Delete(PricingPlanFeature);
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
