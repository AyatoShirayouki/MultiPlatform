using Base.ManagementService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Repository.Implementations;
using Users_ApplicationService.DTOs.Education.EducationDetails;
using Users_Data.Entities.Education.EducationDetails;
using Users_Repository.Implementations.EntityRepositories.Education.EducationDetails;
using Users_Repository.Implementations.EntityRepositories.Education;
using Users_Data.Entities.Education;

namespace Users_ApplicationService.Implementations.Education.EducationDetails
{
    public class EducationalFacilityTypesManagementService : IBaseManagementService
    {
        public static async Task<List<EducationalFacilityDTO>> GetAll()
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                EducationalFacilitiesRepository educationalFacilitiesRepo = new EducationalFacilitiesRepository(unitOfWork);
                List<EducationalFacility> educationalFacilities = await educationalFacilitiesRepo.GetAll();

                List<EducationalFacilityDTO> educationalFacilitiesDTO = new List<EducationalFacilityDTO>();

                if (educationalFacilities != null)
                {
                    foreach (var item in educationalFacilities)
                    {
                        educationalFacilitiesDTO.Add(new EducationalFacilityDTO
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Website = item.Website,
                            EducationalFacilityTypeId = item.EducationalFacilityTypeId,
                            CountryId = item.CountryId,
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return educationalFacilitiesDTO;
            }
        }

        public static async Task<EducationalFacilityDTO> GetById(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                EducationalFacilitiesRepository educationalFacilitiesRepo = new EducationalFacilitiesRepository(unitOfWork);
                EducationalFacilityDTO EducationalFacilityDTO = new EducationalFacilityDTO();

                EducationalFacility EducationalFacility = await educationalFacilitiesRepo.GetById(id);

                if (EducationalFacility != null)
                {
                    EducationalFacilityDTO.Id = EducationalFacility.Id;
                    EducationalFacilityDTO.Name = EducationalFacility.Name;
                    EducationalFacilityDTO.Website = EducationalFacility.Website;
                    EducationalFacilityDTO.EducationalFacilityTypeId = EducationalFacility.EducationalFacilityTypeId;
                    EducationalFacilityDTO.CountryId = EducationalFacility.CountryId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return EducationalFacilityDTO;
            }
        }

        public static async Task<EducationalFacilityDTO> GetEducationalFacilityByName(string name)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();
                EducationalFacilitiesRepository educationalFacilitiesRepo = new EducationalFacilitiesRepository();
                EducationalFacility EducationalFacility = await educationalFacilitiesRepo.GetFirstOrDefault(u => u.Name.Contains(name));
                EducationalFacilityDTO EducationalFacilityDTO = new EducationalFacilityDTO();

                if (EducationalFacility != null)
                {
                    EducationalFacilityDTO = new EducationalFacilityDTO
                    {
                        Id = EducationalFacility.Id,
                        Name = EducationalFacility.Name,
                        Website = EducationalFacility.Website,
                        EducationalFacilityTypeId = EducationalFacility.EducationalFacilityTypeId,
                        CountryId = EducationalFacility.CountryId
                    };

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return EducationalFacilityDTO;
            }
        }

        public static async Task Save(EducationalFacilityDTO EducationalFacilityDTO)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                EducationalFacilitiesRepository educationalFacilitiesRepo = new EducationalFacilitiesRepository(unitOfWork);
                EducationalFacility EducationalFacility = new EducationalFacility();

                if (EducationalFacilityDTO != null)
                {
                    if (EducationalFacilityDTO.Id == 0)
                    {
                        EducationalFacility = new EducationalFacility
                        {
                            Name = EducationalFacilityDTO.Name,
                            Website = EducationalFacilityDTO.Website,
                            EducationalFacilityTypeId = EducationalFacilityDTO.EducationalFacilityTypeId,
                            CountryId = EducationalFacilityDTO.CountryId
                        };
                    }
                    else
                    {
                        EducationalFacility = new EducationalFacility
                        {
                            Id = EducationalFacilityDTO.Id,
                            Name = EducationalFacilityDTO.Name,
                            Website = EducationalFacilityDTO.Website,
                            EducationalFacilityTypeId = EducationalFacilityDTO.EducationalFacilityTypeId,
                            CountryId = EducationalFacilityDTO.CountryId
                        };
                    }

                    await educationalFacilitiesRepo.Save(EducationalFacility);

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

                EducationalFacilitiesRepository educationalFacilitiesRepo = new EducationalFacilitiesRepository(unitOfWork);
                UserEducationsRepository userEducationsRepo = new UserEducationsRepository(unitOfWork);

                EducationalFacility EducationalFacility = await educationalFacilitiesRepo.GetById(id);

                if (EducationalFacility != null)
                {
                    List<UserEducation> userEducations = await userEducationsRepo.GetAll(a => a.EducationalFacilityId == EducationalFacility.Id);
                    foreach (var education in userEducations)
                    {
                        await userEducationsRepo.Delete(education);
                    }

                    await educationalFacilitiesRepo.Delete(EducationalFacility);
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
