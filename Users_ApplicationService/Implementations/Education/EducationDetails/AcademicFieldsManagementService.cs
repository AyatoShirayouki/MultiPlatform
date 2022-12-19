using Base.ManagementService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_ApplicationService.DTOs.Education;
using Users_Data.Entities.Education;
using Users_Repository.Implementations.EntityRepositories.Education;
using Users_Repository.Implementations;
using Users_ApplicationService.DTOs.Education.EducationDetails;
using Users_Data.Entities.Education.EducationDetails;
using Users_Repository.Implementations.EntityRepositories.Education.EducationDetails;

namespace Users_ApplicationService.Implementations.Education.EducationDetails
{
    public class AcademicFieldsManagementService : IBaseManagementService
    {
        public static async Task<List<AcademicFieldDTO>> GetAll()
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                
                AcademicFileldsRepository AcademicFieldsRepo = new AcademicFileldsRepository(unitOfWork);
                List<AcademicField> AcademicFields = await AcademicFieldsRepo.GetAll();

                List<AcademicFieldDTO> AcademicFieldsDTO = new List<AcademicFieldDTO>();

                if (AcademicFields != null)
                {
                    foreach (var item in AcademicFields)
                    {
                        AcademicFieldsDTO.Add(new AcademicFieldDTO
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
                return AcademicFieldsDTO;
            }
        }

        public static async Task<AcademicFieldDTO> GetById(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                AcademicFileldsRepository AcademicFieldsRepo = new AcademicFileldsRepository(unitOfWork);
                AcademicFieldDTO AcademicFieldDTO = new AcademicFieldDTO();

                AcademicField AcademicField = await AcademicFieldsRepo.GetById(id);

                if (AcademicField != null)
                {
                    AcademicFieldDTO.Id = AcademicField.Id;
                    AcademicFieldDTO.Name = AcademicField.Name;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return AcademicFieldDTO;
            }
        }

        public static async Task<AcademicFieldDTO> GetAcademicFieldByName(string name)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();
                AcademicFileldsRepository AcademicFieldsRepo = new AcademicFileldsRepository();
                AcademicField AcademicField = await AcademicFieldsRepo.GetFirstOrDefault(u => u.Name.Contains(name));
                AcademicFieldDTO AcademicFieldDTO = new AcademicFieldDTO();

                if (AcademicField != null)
                {
                    AcademicFieldDTO = new AcademicFieldDTO
                    {
                        Id = AcademicField.Id,
                        Name = AcademicField.Name,
                    };

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return AcademicFieldDTO;
            }
        }

        public static async Task Save(AcademicFieldDTO AcademicFieldDTO)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                AcademicFileldsRepository AcademicFieldsRepo = new AcademicFileldsRepository(unitOfWork);
                AcademicField AcademicField = new AcademicField();

                if (AcademicFieldDTO != null)
                {
                    if (AcademicFieldDTO.Id == 0)
                    {
                        AcademicField = new AcademicField
                        {
                            Name = AcademicFieldDTO.Name,
                        };
                    }
                    else
                    {
                        AcademicField = new AcademicField
                        {
                            Id = AcademicFieldDTO.Id,
                            Name = AcademicFieldDTO.Name,
                        };
                    }

                    await AcademicFieldsRepo.Save(AcademicField);

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

                AcademicFileldsRepository AcademicFieldsRepo = new AcademicFileldsRepository(unitOfWork);
                UserEducationsRepository userEducationsRepo = new UserEducationsRepository(unitOfWork);

                AcademicField AcademicField = await AcademicFieldsRepo.GetById(id);

                if (AcademicField != null)
                {
                    List<UserEducation> userEducations = await userEducationsRepo.GetAll(a => a.AcademicFieldId == AcademicField.Id);
                    foreach (var education in userEducations)
                    {
                        await userEducationsRepo.Delete(education);
                    }

                    await AcademicFieldsRepo.Delete(AcademicField);

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
