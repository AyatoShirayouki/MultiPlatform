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
                            Name = item.name,
                            CountryId = item.country_id,
                            CountryCode = item.country_code,
                            Type = item.type,
                            Latitude = item.latitude,
                            Longitude = item.longitude
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
                    RegionDTO.Name = Region.name;
                    RegionDTO.CountryId = Region.country_id;
					RegionDTO.CountryCode = Region.country_code;
                    RegionDTO.Type = Region.type;
                    RegionDTO.Latitude = Region.latitude;
                    RegionDTO.Longitude = Region.longitude;

					unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return RegionDTO;
            }
        }

        public static async Task<List<RegionDTO>> GetRegionsByCountryId(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                RegionsRepository RegionsRepo = new RegionsRepository();

                List<Region> r = await RegionsRepo.GetAll();

                List<Region> Regions = await RegionsRepo.GetAll(u => u.country_id == id);
                List<RegionDTO> RegionDTOs = new List<RegionDTO>();

                if (Regions != null)
                {
                    for (int i = 0; i < Regions.Count; i++)
                    {
						RegionDTO regionDTO = new RegionDTO
						{
							Id = Regions[i].Id,
							Name = Regions[i].name,
							CountryId = Regions[i].country_id,
							CountryCode = Regions[i].country_code,
							Type = Regions[i].type,
							Latitude = Regions[i].latitude,
							Longitude = Regions[i].longitude
						};

                        RegionDTOs.Add(regionDTO);
					}
                    

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return RegionDTOs;
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
                            name = RegionDTO.Name,
                            country_id = RegionDTO.CountryId,
                            country_code = RegionDTO.CountryCode,
                            type = RegionDTO.Type,
                            latitude = RegionDTO.Latitude,
                            longitude = RegionDTO.Longitude
                        };
                    }
                    else
                    {
                        Region = new Region
                        {
                            Id = RegionDTO.Id,
                            name = RegionDTO.Name,
                            country_id = RegionDTO.CountryId,
							country_code = RegionDTO.CountryCode,
							type = RegionDTO.Type,
							latitude = RegionDTO.Latitude,
							longitude = RegionDTO.Longitude,
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

                    List<City> cities = await citiesRepo.GetAll(s => s.region_id == Region.Id);
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
