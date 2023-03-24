using Base.ManagementService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
                            Name = item.name,
                            RegionId = item.region_id,
                            RegionCode = item.region_code,
                            CountryId = item.country_id,
                            CountryCode = item.country_code,
                            Latitude = item.latitude,
                            Longitude = item.longitude,
                            WikiDataId = item.wikiDataId
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
                    cityDTO.Name = city.name;
                    cityDTO.RegionId = city.region_id;
					cityDTO.RegionCode = city.region_code;
                    cityDTO.CountryId = city.country_id;
                    cityDTO.CountryCode = city.country_code;
                    cityDTO.Latitude = city.latitude;
                    cityDTO.Longitude = city.longitude;
                    cityDTO.WikiDataId = city.wikiDataId;


					unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return cityDTO;
            }
        }

        public static async Task<List<CityDTO>> GetCitiesByRegionAndCountryId(int regionId, int countryId)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                CitiesRepository citiesRepo = new CitiesRepository();
                RegionsRepository regionsRepo = new RegionsRepository();

				List<Region> regions = new List<Region>();
                
                List<City> cities = await citiesRepo.GetAll(c => c.region_id == regionId && c.country_id == countryId);

                List<CityDTO> cityDTOs = new List<CityDTO>();

                if (cities != null)
                {
                    for (int i = 0; i < cities.Count; i++)
                    {
						CityDTO cityDTO = new CityDTO
						{
							Id = cities[i].Id,
							Name = cities[i].name,
							RegionId = cities[i].region_id,
							RegionCode = cities[i].region_code,
							CountryId = cities[i].country_id,
							CountryCode = cities[i].country_code,
							Latitude = cities[i].latitude,
							Longitude = cities[i].longitude,
							WikiDataId = cities[i].wikiDataId
						};

                        cityDTOs.Add(cityDTO);
					}
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return cityDTOs;
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
                                name = cityDTO.Name,
                                region_id = cityDTO.RegionId,
                                region_code = cityDTO.RegionCode,
                                country_id = cityDTO.CountryId,
                                country_code = cityDTO.CountryCode,
                                latitude = cityDTO.Latitude,
                                longitude = cityDTO.Longitude,
                                wikiDataId = cityDTO.WikiDataId
                            };
                        }
                        else
                        {
                            city = new City
                            {
                                Id = cityDTO.Id,
                                name = cityDTO.Name,
                                region_id = cityDTO.RegionId,
								region_code = cityDTO.RegionCode,
								country_id = cityDTO.CountryId,
								country_code = cityDTO.CountryCode,
								latitude = cityDTO.Latitude,
								longitude = cityDTO.Longitude,
								wikiDataId = cityDTO.WikiDataId
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
