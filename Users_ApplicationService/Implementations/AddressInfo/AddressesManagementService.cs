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
    public class AddressesManagementService : IBaseManagementService
    {
        public static async Task<List<AddressDTO>> GetAll()
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                AddressesRepository addressesRepo = new AddressesRepository(unitOfWork);
                List<Address> addresses = await addressesRepo.GetAll();

                List<AddressDTO> addressesDTO = new List<AddressDTO>();

                if (addresses != null)
                {
                    foreach (var item in addresses)
                    {
                        addressesDTO.Add(new AddressDTO
                        {
                            Id = item.Id,
                            CountryId = item.CountryId,
                            CityId = item.CityId,
                            RegionId = item.RegionId
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return addressesDTO;
            }
        }

        public static async Task<AddressDTO> GetById(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                AddressesRepository addressesRepo = new AddressesRepository(unitOfWork);
                AddressDTO addressDTO = new AddressDTO();

                Address address = await addressesRepo.GetById(id);

                if (address != null)
                {
                    addressDTO.Id = address.Id;
                    addressDTO.CountryId = address.CountryId;
                    addressDTO.CityId = address.CityId;
                    addressDTO.RegionId = address.RegionId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return addressDTO;
            }
        }

        public static async Task<List<AddressDTO>> GetAddressesByCountryId(int countryId)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();
                AddressesRepository addressesRepo = new AddressesRepository();
                List<Address> addresses = await addressesRepo.GetAll(u => u.CountryId == countryId);
                List<AddressDTO> addressesDTO = new List<AddressDTO>();

                if (addresses != null)
                {
                    foreach (var address in addressesDTO)
                    {
                        AddressDTO addressDTO = new AddressDTO
                        {
                            Id = address.Id,
                            CountryId = address.CountryId,
                            CityId = address.CityId,
                            RegionId = address.RegionId
                        };

                        addressesDTO.Add(addressDTO);
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return addressesDTO;
            }
        }

        public static async Task<List<AddressDTO>> GetAddressByCityId(int CityId)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();
                AddressesRepository addressesRepo = new AddressesRepository();
                List<Address> addresses = await addressesRepo.GetAll(u => u.CityId == CityId);
                List<AddressDTO> addressesDTO = new List<AddressDTO>();

                if (addresses != null)
                {
                    foreach (var address in addressesDTO)
                    {
                        AddressDTO addressDTO = new AddressDTO
                        {
                            Id = address.Id,
                            CountryId = address.CountryId,
                            CityId = address.CityId,
                            RegionId = address.RegionId
                        };

                        addressesDTO.Add(addressDTO);
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return addressesDTO;
            }
        }

        public static async Task<List<AddressDTO>> GetAddressByRegionId(int RegionId)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();
                AddressesRepository addressesRepo = new AddressesRepository();
                List<Address> addresses = await addressesRepo.GetAll(u => u.RegionId == RegionId);
                List<AddressDTO> addressesDTO = new List<AddressDTO>();

                if (addresses != null)
                {
                    foreach (var address in addressesDTO)
                    {
                        AddressDTO addressDTO = new AddressDTO
                        {
                            Id = address.Id,
                            CountryId = address.CountryId,
                            CityId = address.CityId,
                            RegionId = address.RegionId
                        };

                        addressesDTO.Add(addressDTO);
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return addressesDTO;
            }
        }

        public static async Task Save(AddressDTO addressDTO)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                AddressesRepository addressesRepo = new AddressesRepository(unitOfWork);
                Address address = new Address();

                if (addressDTO != null)
                {
                    CountriesRepository countriesRepo = new CountriesRepository(unitOfWork);
                    Country country = await countriesRepo.GetById(addressDTO.CountryId);

                    CitiesRepository citiesRepo = new CitiesRepository(unitOfWork);
                    City city = await citiesRepo.GetById(addressDTO.CityId);

                    RegionsRepository regionsRepo = new RegionsRepository(unitOfWork);
                    Region region = await regionsRepo.GetById(addressDTO.RegionId);

                    if (country != null && city != null && region != null)
                    {
                        if (addressDTO.Id == 0)
                        {
                            address = new Address
                            {
                                CountryId = addressDTO.CountryId,
                                CityId = addressDTO.CityId,
                                RegionId = addressDTO.RegionId
                            };
                        }
                        else
                        {
                            address = new Address
                            {
                                Id = addressDTO.Id,
                                CountryId = addressDTO.CountryId,
                                CityId = addressDTO.CityId,
                                RegionId = addressDTO.RegionId
                            };
                        }
                    }

                    await addressesRepo.Save(address);
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

                AddressesRepository addressesRepo = new AddressesRepository(unitOfWork);

                Address address = await addressesRepo.GetById(id);

                if (address != null)
                {
                    await addressesRepo.Delete(address);
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
