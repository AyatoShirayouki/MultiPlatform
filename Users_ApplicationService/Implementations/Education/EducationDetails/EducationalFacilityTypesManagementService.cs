using Base.ManagementService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_ApplicationService.DTOs.Education.EducationDetails;
using Users_Data.Entities.Education.EducationDetails;
using Users_Data.Entities.Education;
using Users_Repository.Implementations.EntityRepositories.Education.EducationDetails;
using Users_Repository.Implementations.EntityRepositories.Education;
using Users_Repository.Implementations;

namespace Users_ApplicationService.Implementations.Education.EducationDetails
{
    public class EducationalFacilityTypeTypesManagementService : IBaseManagementService
    {
        public static async Task<List<EducationalFacilityTypeDTO>> GetAll()
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                EducationalFacilityTypesRepository EducationalFacilityTypesRepo = new EducationalFacilityTypesRepository(unitOfWork);
                List<EducationalFacilityType> EducationalFacilityTypes = await EducationalFacilityTypesRepo.GetAll();

                List<EducationalFacilityTypeDTO> EducationalFacilityTypesDTO = new List<EducationalFacilityTypeDTO>();

                if (EducationalFacilityTypes != null)
                {
                    foreach (var item in EducationalFacilityTypes)
                    {
                        EducationalFacilityTypesDTO.Add(new EducationalFacilityTypeDTO
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
                return EducationalFacilityTypesDTO;
            }
        }

        public static async Task<EducationalFacilityTypeDTO> GetById(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                EducationalFacilityTypesRepository EducationalFacilityTypesRepo = new EducationalFacilityTypesRepository(unitOfWork);
                EducationalFacilityTypeDTO EducationalFacilityTypeDTO = new EducationalFacilityTypeDTO();

                EducationalFacilityType EducationalFacilityType = await EducationalFacilityTypesRepo.GetById(id);

                if (EducationalFacilityType != null)
                {
                    EducationalFacilityTypeDTO.Id = EducationalFacilityType.Id;
                    EducationalFacilityTypeDTO.Name = EducationalFacilityType.Name;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return EducationalFacilityTypeDTO;
            }
        }

        public static async Task<EducationalFacilityTypeDTO> GetEducationalFacilityTypeByName(string name)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();
                EducationalFacilityTypesRepository EducationalFacilityTypesRepo = new EducationalFacilityTypesRepository();
                EducationalFacilityType EducationalFacilityType = await EducationalFacilityTypesRepo.GetFirstOrDefault(u => u.Name.Contains(name));
                EducationalFacilityTypeDTO EducationalFacilityTypeDTO = new EducationalFacilityTypeDTO();

                if (EducationalFacilityType != null)
                {
                    EducationalFacilityTypeDTO = new EducationalFacilityTypeDTO
                    {
                        Id = EducationalFacilityType.Id,
                        Name = EducationalFacilityType.Name
                    };

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return EducationalFacilityTypeDTO;
            }
        }

        public static async Task Save(EducationalFacilityTypeDTO EducationalFacilityTypeDTO)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                EducationalFacilityTypesRepository EducationalFacilityTypesRepo = new EducationalFacilityTypesRepository(unitOfWork);
                EducationalFacilityType EducationalFacilityType = new EducationalFacilityType();

                if (EducationalFacilityTypeDTO != null)
                {
                    if (EducationalFacilityTypeDTO.Id == 0)
                    {
                        EducationalFacilityType = new EducationalFacilityType
                        {
                            Name = EducationalFacilityTypeDTO.Name
                        };
                    }
                    else
                    {
                        EducationalFacilityType = new EducationalFacilityType
                        {
                            Id = EducationalFacilityTypeDTO.Id,
                            Name = EducationalFacilityTypeDTO.Name
                        };
                    }

                    await EducationalFacilityTypesRepo.Save(EducationalFacilityType);

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

                EducationalFacilityTypesRepository EducationalFacilityTypesRepo = new EducationalFacilityTypesRepository(unitOfWork);
                EducationalFacilitiesRepository educationalFacilitiesRepo = new EducationalFacilitiesRepository(unitOfWork);

                EducationalFacilityType EducationalFacilityType = await EducationalFacilityTypesRepo.GetById(id);

                if (EducationalFacilityType != null)
                {
                    List<EducationalFacility> eaducationalFacilities = await educationalFacilitiesRepo.GetAll(a => a.EducationalFacilityTypeId == EducationalFacilityType.Id);
                    foreach (var educationalFacility in eaducationalFacilities)
                    {
                        await educationalFacilitiesRepo.Delete(educationalFacility);
                    }

                    await EducationalFacilityTypesRepo.Delete(EducationalFacilityType);
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
