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
                            Name = item.name,
                            Iso3 = item.iso3,
                            NumericCode = item.numeric_code,
                            Iso2 = item.iso2,
                            PhoneCode = item.phonecode,
                            Capital = item.capital,
                            Currency = item.currency,
                            CurrencyName = item.currency_name,
                            CurrencySymbol = item.currency_symbol,
                            Tld = item.tld,
                            Native = item.native,
                            Region = item.region,
                            Subregion = item.subregion,
                            Timezones = item.timezones,
                            Translations = item.translations,
                            Latitude = item.latitude,
                            Longitude = item.longitude,
                            Emoji = item.emoji,
                            EmojiU = item.emojiU
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
                    countryDTO.Name = country.name;
					countryDTO.Iso3 = country.iso3;
                    countryDTO.NumericCode = country.numeric_code;
                    countryDTO.Iso2 = country.iso2;
                    countryDTO.PhoneCode = country.phonecode;
                    countryDTO.Capital = country.capital;
                    countryDTO.Currency = country.currency;
                    countryDTO.CurrencyName = country.currency_name;
                    countryDTO.CurrencySymbol = country.currency_symbol;
                    countryDTO.Tld = country.tld;
                    countryDTO.Native = country.native;
                    countryDTO.Region = country.region;
                    countryDTO.Subregion = country.subregion;
                    countryDTO.Timezones = country.timezones;
                    countryDTO.Translations = country.translations;
                    countryDTO.Latitude = country.latitude;
                    countryDTO.Longitude = country.longitude;
                    countryDTO.Emoji = country.emoji;
                    countryDTO.EmojiU = country.emojiU;

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
                Country country = await countriesRepo.GetFirstOrDefault(u => u.name.Contains(name));
                CountryDTO countryDTO = new CountryDTO();

                if (country != null)
                {
                    countryDTO = new CountryDTO
                    {
                        Id = country.Id,
                        Name = country.name,
						Iso3 = country.iso3,
						NumericCode = country.numeric_code,
						Iso2 = country.iso2,
						PhoneCode = country.phonecode,
						Capital = country.capital,
						Currency = country.currency,
						CurrencyName = country.currency_name,
						CurrencySymbol = country.currency_symbol,
						Tld = country.tld,
						Native = country.native,
						Region = country.region,
						Subregion = country.subregion,
						Timezones = country.timezones,
						Translations = country.translations,
						Latitude = country.latitude,
						Longitude = country.longitude,
						Emoji = country.emoji,
						EmojiU = country.emojiU
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
                            name = countryDTO.Name,
							iso3 = countryDTO.Iso3,
							numeric_code = countryDTO.NumericCode,
							iso2 = countryDTO.Iso2,
							phonecode = countryDTO.PhoneCode,
							capital = countryDTO.Capital,
							currency = countryDTO.Currency,
							currency_name = countryDTO.CurrencyName,
							currency_symbol = countryDTO.CurrencySymbol,
							tld = countryDTO.Tld,
							native = countryDTO.Native,
							region = countryDTO.Region,
							subregion = countryDTO.Subregion,
							timezones = countryDTO.Timezones,
							translations = countryDTO.Translations,
							latitude = countryDTO.Latitude,
							longitude = countryDTO.Longitude,
							emoji = countryDTO.Emoji,
							emojiU = countryDTO.EmojiU
						};
                    }
                    else
                    {
                        country = new Country
                        {
                            Id = countryDTO.Id,
							name = countryDTO.Name,
							iso3 = countryDTO.Iso3,
							numeric_code = countryDTO.NumericCode,
							iso2 = countryDTO.Iso2,
							phonecode = countryDTO.PhoneCode,
							capital = countryDTO.Capital,
							currency = countryDTO.Currency,
							currency_name = countryDTO.CurrencyName,
							currency_symbol = countryDTO.CurrencySymbol,
							tld = countryDTO.Tld,
							native = countryDTO.Native,
							region = countryDTO.Region,
							subregion = countryDTO.Subregion,
							timezones = countryDTO.Timezones,
							translations = countryDTO.Translations,
							latitude = countryDTO.Latitude,
							longitude = countryDTO.Longitude,
							emoji = countryDTO.Emoji,
							emojiU = countryDTO.EmojiU
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

                    List<Region> regions = await regionsRepo.GetAll(c => c.country_id == country.Id);
                    foreach (var region in regions)
                    {
                        List<City> cities = await citiesRepo.GetAll(s => s.region_id == region.Id);
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
