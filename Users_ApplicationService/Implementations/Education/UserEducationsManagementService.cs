using Base.ManagementService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_ApplicationService.DTOs.Education;
using Users_Data.Entities.Education;
using Users_Data.Entities;
using Users_Repository.Implementations.EntityRepositories.Education;
using Users_Repository.Implementations.EntityRepositories;
using Users_Repository.Implementations;

namespace Users_ApplicationService.Implementations.Education
{
    public class UserEducationsManagementService : IBaseManagementService
    {
        public static async Task<List<UserEducationDTO>> GetAll()
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                UserEducationsRepository UserEducationsRepo = new UserEducationsRepository(unitOfWork);
                List<UserEducation> UserEducations = await UserEducationsRepo.GetAll();

                List<UserEducationDTO> UserEducationsDTO = new List<UserEducationDTO>();

                if (UserEducations != null)
                {
                    foreach (var item in UserEducations)
                    {
                        UserEducationsDTO.Add(new UserEducationDTO
                        {
                            Id = item.Id,
                            Speacialty = item.Speacialty,
                            UserId = item.UserId,
                            DegreeId = item.DegreeId,
                            AcademicFieldId = item.AcademicFieldId,
                            EducationalFacilityId = item.EducationalFacilityId,
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }

                return UserEducationsDTO;
            }
        }

        public static async Task<UserEducationDTO> GetById(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                UserEducationsRepository UserEducationsRepo = new UserEducationsRepository(unitOfWork);
                UserEducationDTO UserEducationDTO = new UserEducationDTO();

                UserEducation UserEducation = await UserEducationsRepo.GetById(id);

                if (UserEducation != null)
                {
                    UserEducationDTO.Id = UserEducation.Id;
                    UserEducationDTO.Speacialty = UserEducation.Speacialty;
                    UserEducationDTO.UserId = UserEducation.UserId;
                    UserEducationDTO.DegreeId = UserEducation.DegreeId;
                    UserEducation.AcademicFieldId = UserEducation.AcademicFieldId;
                    UserEducationDTO.EducationalFacilityId = UserEducation.EducationalFacilityId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }

                return UserEducationDTO;
            }
        }

        public static async Task Save(UserEducationDTO UserEducationDTO)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                UserEducationsRepository UserEducationsRepo = new UserEducationsRepository(unitOfWork);
                UserEducation UserEducation = new UserEducation();

                if (UserEducationDTO != null)
                {
                    UsersRepository usersRepo = new UsersRepository(unitOfWork);
                    User user = await usersRepo.GetById(UserEducationDTO.UserId);

                    if (user != null)
                    {
                        if (UserEducationDTO.Id == 0)
                        {
                            UserEducation = new UserEducation
                            {
                                Speacialty = UserEducationDTO.Speacialty,
                                UserId = UserEducationDTO.UserId,
                                DegreeId = UserEducationDTO.DegreeId,
                                AcademicFieldId = UserEducationDTO.AcademicFieldId,
                                EducationalFacilityId = UserEducationDTO.EducationalFacilityId
                            };
                        }
                        else
                        {
                            UserEducation = new UserEducation
                            {
                                Id = UserEducationDTO.Id,
                                Speacialty = UserEducationDTO.Speacialty,
                                UserId = UserEducationDTO.UserId,
                                DegreeId = UserEducationDTO.DegreeId,
                                AcademicFieldId = UserEducationDTO.AcademicFieldId,
                                EducationalFacilityId = UserEducationDTO.EducationalFacilityId,
                            };
                        }
                    }

                    await UserEducationsRepo.Save(UserEducation);
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

                UserEducationsRepository UserEducationsRepo = new UserEducationsRepository(unitOfWork);

                UserEducation UserEducation = await UserEducationsRepo.GetById(id);

                if (UserEducation != null)
                {
                    await UserEducationsRepo.Delete(UserEducation);
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
