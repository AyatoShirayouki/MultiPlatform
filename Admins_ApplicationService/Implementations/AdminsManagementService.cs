using Admins_ApplicationService.DTOs;
using Admins_Data.Entities;
using Admins_Repository.Implementations;
using Admins_Repository.Implementations.EntityRepositories;
using Base.ManagementService;
using ScriptExecutor.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Admins_ApplicationService.Implementations
{
    public class AdminsManagementService : IBaseManagementService
    {
        public static async Task<List<AdminDTO>> GetAll()
        {
            using (AdminsUnitOfWork unitOfWork = new AdminsUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                AdminsRepository adminsRepo = new AdminsRepository(unitOfWork);
                List<Admin> admins = await adminsRepo.GetAll();

                List<AdminDTO> adminsDTO = new List<AdminDTO>();

                if (admins != null)
                {
                    foreach (var item in admins)
                    {
                        adminsDTO.Add(new AdminDTO
                        {
                            Id = item.Id,
                            Email = item.Email,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Password = item.Password,
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return adminsDTO;
            }
        }

        public static async Task<AdminDTO> GetById(int id)
        {
            using (AdminsUnitOfWork unitOfWork = new AdminsUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                AdminsRepository adminsRepo = new AdminsRepository(unitOfWork);
                AdminDTO adminDTO = new AdminDTO();

                Admin admin = await adminsRepo.GetById(id);

                if (admin != null)
                {
                    adminDTO.Id = admin.Id;
                    adminDTO.Email = admin.Email;
                    adminDTO.Password = admin.Password;
                    adminDTO.FirstName = admin.FirstName;
                    adminDTO.LastName = admin.LastName;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return adminDTO;
            }
        }

        public static async Task Save(AdminDTO adminDTO)
        {
            using (AdminsUnitOfWork unitOfWork = new AdminsUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                AdminsRepository adminsRepo = new AdminsRepository(unitOfWork);
                Admin admin = new Admin();

                if (adminDTO != null)
                {
                    if (adminDTO.Id == 0)
                    {
                        admin = new Admin
                        {
                            Email = adminDTO.Email,
                            FirstName = adminDTO.FirstName,
                            LastName = adminDTO.LastName,
                            Password = adminDTO.Password,
                        };
                    }
                    else
                    {
                        admin = new Admin
                        {
                            Id = adminDTO.Id,
                            Email = adminDTO.Email,
                            FirstName = adminDTO.FirstName,
                            LastName = adminDTO.LastName,
                            Password = adminDTO.Password,
                        };
                    }

                    await adminsRepo.Save(admin);
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
            }
        }

        public static async Task<bool> VerifyEmail(string email)
        {
            bool exists = true;

            using (AdminsUnitOfWork unitOfWork = new AdminsUnitOfWork())
            {
                unitOfWork.BeginTransaction();
                AdminsRepository adminsRepo = new AdminsRepository();
                Admin admin = await adminsRepo.GetFirstOrDefault(u => u.Email == email);

                if (admin == null)
                {
                    exists = false;
                }

                unitOfWork.Commit();
                return exists;
            }
        }

        public static async Task Delete(int id)
        {
            using (AdminsUnitOfWork unitOfWork = new AdminsUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                AdminsRepository adminsRepo = new AdminsRepository(unitOfWork);
                RefreshAdminTokensRepository refreshAdminTokensRepo = new RefreshAdminTokensRepository(unitOfWork);
                AdminFilesRepository adminFilesRepo = new AdminFilesRepository(unitOfWork);

                Admin admin = await adminsRepo.GetById(id);

                if (admin != null)
                {
                    List<RefreshAdminToken> refreshAdminTokens = await refreshAdminTokensRepo.GetAll(t => t.AdminId == admin.Id);
                    foreach (var token in refreshAdminTokens)
                    {
                        await refreshAdminTokensRepo.Delete(token);
                    }

                    List<AdminFile> adminFiles = await adminFilesRepo.GetAll(i => i.AdminId == admin.Id);
                    foreach (var image in adminFiles)
                    {
                        await adminFilesRepo.Delete(image);
                    }

                    await adminsRepo.Delete(admin);
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
            }
        }

        public static async Task<int> FillCountriesRegionsAndCitiesActionMSSQL()
        {
            using (AdminsUnitOfWork unitOfWork = new AdminsUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                MSSqlScriptExecutor executor = new MSSqlScriptExecutor();
                int result = await executor.RunCountriesRegionsCitiesScripts();

                if (result == 1)
                {
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }

                return result;
            }
        }

		public static async Task<int> FillCountriesRegionsAndCitiesActionPostgreSQL()
		{
            using (AdminsUnitOfWork unitOfWork = new AdminsUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                PostgreSQLScriptExecutor executor = new PostgreSQLScriptExecutor();
                int result = await executor.RunCountriesRegionsCitiesScripts();

                if (result == 1)
                {
                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }

                return result;
            }
		}
	}
}
