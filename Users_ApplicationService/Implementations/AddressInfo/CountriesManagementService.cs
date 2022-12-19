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
    public class CountriesManagementService : IBaseManagementService
    {
        public static async Task<List<CountryDTO>> GetAll()
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                CountriesRepository countriesRepo = new CountriesRepository(unitOfWork);
                List<Country> countries = await countriesRepo.GetAll();

                List<CountryDTO> countriesDTO = new List<CountryDTO>();

                if (countries != null)
                {
                    foreach (var item in countries)
                    {
                        countriesDTO.Add(new CountryDTO
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Code = item.Code,
                            Language = item.Language
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return countriesDTO;
            }
        }

        public static async Task<CountryDTO> GetById(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                CountriesRepository countriesRepo = new CountriesRepository(unitOfWork);
                CountryDTO countryDTO = new CountryDTO();

                Country country = await countriesRepo.GetById(id);

                if (country != null)
                {
                    countryDTO.Id = country.Id;
                    countryDTO.Name = country.Name;
                    countryDTO.Code = country.Code;
                    countryDTO.Language = country.Language;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return countryDTO;
            }
        }

        public static async Task<CountryDTO> GetCountryByName(string name)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();
                CountriesRepository countriesRepo = new CountriesRepository();
                Country country = await countriesRepo.GetFirstOrDefault(u => u.Name.Contains(name));
                CountryDTO countryDTO = new CountryDTO();

                if (country != null)
                {
                    countryDTO = new CountryDTO
                    {
                        Id = country.Id,
                        Name = country.Name,
                        Code = country.Code,
                        Language = country.Language
                    };

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return countryDTO;
            }
        }

        public static async Task Save(CountryDTO countryDTO)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                CountriesRepository countriesRepo = new CountriesRepository(unitOfWork);
                Country country = new Country();

                if (countryDTO != null)
                {
                    if (countryDTO.Id == 0)
                    {
                        country = new Country
                        {
                            Name = countryDTO.Name,
                            Code = countryDTO.Code,
                            Language = countryDTO.Language
                        };
                    }
                    else
                    {
                        country = new Country
                        {
                            Id = countryDTO.Id,
                            Name = countryDTO.Name,
                            Code = countryDTO.Code,
                            Language = countryDTO.Language
                        };
                    }

                    await countriesRepo.Save(country);

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

                CountriesRepository countriesRepo = new CountriesRepository(unitOfWork);
                CitiesRepository citiesRepo = new CitiesRepository(unitOfWork);
                RegionsRepository regionsRepo = new RegionsRepository(unitOfWork);
                AddressesRepository addressesRepo = new AddressesRepository(unitOfWork);

                Country country = await countriesRepo.GetById(id);

                if (country != null)
                {
                    List<Address> addresses = await addressesRepo.GetAll(a => a.CountryId == country.Id);
                    foreach (var address in addresses)
                    {
                        await addressesRepo.Delete(address);
                    }

                    List<Region> regions = await regionsRepo.GetAll(c => c.CountryId == country.Id);
                    foreach (var region in regions)
                    {
                        List<City> cities = await citiesRepo.GetAll(s => s.RegionId == region.Id);
                        foreach (var city in cities)
                        {
                            await citiesRepo.Delete(city);
                        }

                        await regionsRepo.Delete(region);
                    }

                    await countriesRepo.Delete(country);

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
