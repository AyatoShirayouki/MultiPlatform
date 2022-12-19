using Base.ManagementService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_ApplicationService.DTOs.AddressInfo;
using Users_Data.Entities.AddressInfo;
using Users_Repository.Implementations;
using Users_Repository.Implementations.EntityRepositories.AddressInfo;

namespace Users_ApplicationService.Implementations.AddressInfo
{
    public class CitiesManagementService : IBaseManagementService
    {
        public static async Task<List<CityDTO>> GetAll()
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                CitiesRepository citiesRepo = new CitiesRepository(unitOfWork);
                List<City> cities = await citiesRepo.GetAll();

                List<CityDTO> citiesDTO = new List<CityDTO>();

                if (cities != null)
                {
                    foreach (var item in cities)
                    {
                        citiesDTO.Add(new CityDTO
                        {
                            Id = item.Id,
                            Name = item.Name,
                            RegionId = item.RegionId
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return citiesDTO;
            }
        }

        public static async Task<CityDTO> GetById(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                CitiesRepository citiesRepo = new CitiesRepository(unitOfWork);
                CityDTO cityDTO = new CityDTO();

                City city = await citiesRepo.GetById(id);

                if (city != null)
                {
                    cityDTO.Id = city.Id;
                    cityDTO.Name = city.Name;
                    cityDTO.RegionId = city.RegionId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return cityDTO;
            }
        }

        public static async Task<CityDTO> GetCityByName(string name)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();
                CitiesRepository citiesRepo = new CitiesRepository();
                City city = await citiesRepo.GetFirstOrDefault(u => u.Name.Contains(name));
                CityDTO cityDTO = new CityDTO();

                if (city != null)
                {
                    cityDTO = new CityDTO
                    {
                        Id = city.Id,
                        Name = city.Name,
                        RegionId = city.RegionId,
                    };

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return cityDTO;
            }
        }

        public static async Task Save(CityDTO cityDTO)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                CitiesRepository citiesRepo = new CitiesRepository(unitOfWork);
                RegionsRepository regionsRepo = new RegionsRepository(unitOfWork);

                Region region = await regionsRepo.GetById(cityDTO.RegionId);
                City city = new City();

                if (cityDTO != null)
                {
                    if (region != null)
                    {
                        if (cityDTO.Id == 0)
                        {
                            city = new City
                            {
                                Name = cityDTO.Name,
                                RegionId = cityDTO.RegionId,
                            };
                        }
                        else
                        {
                            city = new City
                            {
                                Id = cityDTO.Id,
                                Name = cityDTO.Name,
                                RegionId = cityDTO.RegionId,
                            };
                        }

                        await citiesRepo.Save(city);

                        unitOfWork.Commit();
                    }
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

                AddressesRepository addressesRepo = new AddressesRepository(unitOfWork);
                CitiesRepository citiesRepo = new CitiesRepository(unitOfWork);

                City city = await citiesRepo.GetById(id);

                if (city != null)
                {
                    List<Address> addresses = await addressesRepo.GetAll(a => a.CityId == city.Id);
                    foreach (var address in addresses)
                    {
                        await addressesRepo.Delete(address);
                    }

                    await citiesRepo.Delete(city);
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
