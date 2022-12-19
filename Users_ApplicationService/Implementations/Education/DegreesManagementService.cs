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
using Users_ApplicationService.DTOs.Education;
using Users_Data.Entities.Education;
using Users_Repository.Implementations.EntityRepositories.Education;

namespace Users_ApplicationService.Implementations.Education
{
    public class DegreesManagementService : IBaseManagementService
    {
        public static async Task<List<DegreeDTO>> GetAll()
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                DegreesRepository DegreesRepo = new DegreesRepository(unitOfWork);
                List<Degree> Degrees = await DegreesRepo.GetAll();

                List<DegreeDTO> DegreesDTO = new List<DegreeDTO>();

                if (Degrees != null)
                {
                    foreach (var item in Degrees)
                    {
                        DegreesDTO.Add(new DegreeDTO
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
                return DegreesDTO;
            }
        }

        public static async Task<DegreeDTO> GetById(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                DegreesRepository DegreesRepo = new DegreesRepository(unitOfWork);
                DegreeDTO DegreeDTO = new DegreeDTO();

                Degree Degree = await DegreesRepo.GetById(id);

                if (Degree != null)
                {
                    DegreeDTO.Id = Degree.Id;
                    DegreeDTO.Name = Degree.Name;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return DegreeDTO;
            }
        }

        public static async Task<DegreeDTO> GetDegreeByName(string name)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();
                DegreesRepository DegreesRepo = new DegreesRepository();
                Degree Degree = await DegreesRepo.GetFirstOrDefault(u => u.Name.Contains(name));
                DegreeDTO DegreeDTO = new DegreeDTO();

                if (Degree != null)
                {
                    DegreeDTO = new DegreeDTO
                    {
                        Id = Degree.Id,
                        Name = Degree.Name,
                    };

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return DegreeDTO;
            }
        }

        public static async Task Save(DegreeDTO DegreeDTO)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                DegreesRepository DegreesRepo = new DegreesRepository(unitOfWork);
                Degree Degree = new Degree();

                if (DegreeDTO != null)
                {
                    if (DegreeDTO.Id == 0)
                    {
                        Degree = new Degree
                        {
                            Name = DegreeDTO.Name,
                        };
                    }
                    else
                    {
                        Degree = new Degree
                        {
                            Id = DegreeDTO.Id,
                            Name = DegreeDTO.Name,
                        };
                    }

                    await DegreesRepo.Save(Degree);

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

                DegreesRepository DegreesRepo = new DegreesRepository(unitOfWork);
                UserEducationsRepository userEducationsRepo = new UserEducationsRepository(unitOfWork);

                Degree Degree = await DegreesRepo.GetById(id);

                if (Degree != null)
                {
                    List<UserEducation> userEducations = await userEducationsRepo.GetAll(a => a.DegreeId == Degree.Id);
                    foreach (var education in userEducations)
                    {
                        await userEducationsRepo.Delete(education);
                    }

                    await DegreesRepo.Delete(Degree);

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
