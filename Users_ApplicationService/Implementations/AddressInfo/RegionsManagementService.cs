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

namespace Users_ApplicationService.Implementations.AddressInfo
{
    public class RegionsManagementService : IBaseManagementService
    {
        public static async Task<List<RegionDTO>> GetAll()
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                RegionsRepository RegionsRepo = new RegionsRepository(unitOfWork);
                List<Region> Regions = await RegionsRepo.GetAll();

                List<RegionDTO> RegionsDTO = new List<RegionDTO>();

                if (Regions != null)
                {
                    foreach (var item in Regions)
                    {
                        RegionsDTO.Add(new RegionDTO
                        {
                            Id = item.Id,
                            Name = item.Name,
                            CountryId = item.CountryId
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return RegionsDTO;
            }
        }

        public static async Task<RegionDTO> GetById(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                RegionsRepository RegionsRepo = new RegionsRepository(unitOfWork);
                RegionDTO RegionDTO = new RegionDTO();

                Region Region = await RegionsRepo.GetById(id);

                if (Region != null)
                {
                    RegionDTO.Id = Region.Id;
                    RegionDTO.Name = Region.Name;
                    RegionDTO.CountryId = Region.CountryId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return RegionDTO;
            }
        }

        public static async Task<RegionDTO> GetRegionByName(string name)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();
                RegionsRepository RegionsRepo = new RegionsRepository();
                Region Region = await RegionsRepo.GetFirstOrDefault(u => u.Name.Contains(name));
                RegionDTO RegionDTO = new RegionDTO();

                if (Region != null)
                {
                    RegionDTO = new RegionDTO
                    {
                        Id = Region.Id,
                        Name = Region.Name,
                        CountryId = Region.CountryId
                    };

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return RegionDTO;
            }
        }

        public static async Task Save(RegionDTO RegionDTO)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                RegionsRepository RegionsRepo = new RegionsRepository(unitOfWork);
                Region Region = new Region();

                if (RegionDTO != null)
                {
                    if (RegionDTO.Id == 0)
                    {
                        Region = new Region
                        {
                            Name = RegionDTO.Name,
                            CountryId = RegionDTO.CountryId
                        };
                    }
                    else
                    {
                        Region = new Region
                        {
                            Id = RegionDTO.Id,
                            Name = RegionDTO.Name,
                            CountryId = RegionDTO.CountryId
                        };
                    }

                    await RegionsRepo.Save(Region);

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

                RegionsRepository RegionsRepo = new RegionsRepository(unitOfWork);
                CitiesRepository citiesRepo = new CitiesRepository(unitOfWork);
                AddressesRepository addressesRepo = new AddressesRepository(unitOfWork);

                Region Region = await RegionsRepo.GetById(id);

                if (Region != null)
                {
                    List<Address> addresses = await addressesRepo.GetAll(a => a.RegionId == Region.Id);
                    foreach (var address in addresses)
                    {
                        await addressesRepo.Delete(address);
                    }

                    List<City> cities = await citiesRepo.GetAll(s => s.RegionId == Region.Id);
                    foreach (var city in cities)
                    {
                            await citiesRepo.Delete(city);
                    }
                    
                    await RegionsRepo.Delete(Region);

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
