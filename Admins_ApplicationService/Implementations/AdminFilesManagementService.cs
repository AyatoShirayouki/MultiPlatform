using Admins_ApplicationService.DTOs;
using Admins_Data.Entities;
using Admins_Repository.Implementations;
using Admins_Repository.Implementations.EntityRepositories;
using Base.ManagementService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admins_ApplicationService.Implementations
{
    public class AdminFilesManagementService : IBaseManagementService
    {
        public static async Task<List<AdminFileDTO>> GetAll()
        {
            using (AdminsUnitOfWork unitOfWork = new AdminsUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                AdminFilesRepository adminFilesRepo = new AdminFilesRepository(unitOfWork);
                List<AdminFile> adminFiles = await adminFilesRepo.GetAll();

                List<AdminFileDTO> adminFilesDTO = new List<AdminFileDTO>();

                if (adminFiles != null)
                {
                    foreach (var item in adminFiles)
                    {
                        adminFilesDTO.Add(new AdminFileDTO
                        {
                            Id = item.Id,
                            AdminId = item.AdminId,
                            IsProfilePhoto = item.IsProfilePhoto,
                            FileName = item.FileName,
                            FileType = item.FileType,
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return adminFilesDTO;
            }
        }

        public static async Task<AdminFileDTO> GetById(int id)
        {
            using (AdminsUnitOfWork unitOfWork = new AdminsUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                AdminFilesRepository adminFilesRepo = new AdminFilesRepository(unitOfWork);
                AdminFileDTO adminFileDTO = new AdminFileDTO();

                AdminFile adminFile = await adminFilesRepo.GetById(id);

                if (adminFile != null)
                {
                    adminFileDTO.Id = adminFile.Id;
                    adminFileDTO.FileName = adminFile.FileName;
                    adminFileDTO.IsProfilePhoto = adminFile.IsProfilePhoto;
                    adminFileDTO.FileType = adminFile.FileType;
                    adminFileDTO.AdminId = adminFile.AdminId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return adminFileDTO;
            }
        }

        public static async Task<List<AdminFileDTO>> GetFilesByParentId(int id)
        {
            using (AdminsUnitOfWork unitOfWork = new AdminsUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                AdminsRepository AdminsRepo = new AdminsRepository(unitOfWork);
                AdminFilesRepository AdminFilesRepo = new AdminFilesRepository(unitOfWork);

                List<AdminFileDTO> AdminFilesDTO = new List<AdminFileDTO>();

                Admin Admin = await AdminsRepo.GetById(id);

                if (Admin != null)
                {
                    List<AdminFile> AdminFiles = await AdminFilesRepo.GetAll();

                    foreach (var item in AdminFiles)
                    {
                        if (item.AdminId == Admin.Id)
                        {
                            AdminFilesDTO.Add(new AdminFileDTO
                            {
                                Id = item.Id,
                                AdminId = item.AdminId,
                                IsProfilePhoto = item.IsProfilePhoto,
                                FileName = item.FileName,
                                FileType = item.FileType,
                            });
                        }
                        continue;
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return AdminFilesDTO;
            }
        }

        public static async Task Save(AdminFileDTO adminFileDTO)
        {
            using (AdminsUnitOfWork unitOfWork = new AdminsUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                AdminFilesRepository adminFilesRepo = new AdminFilesRepository(unitOfWork);
                AdminFile adminFile = new AdminFile();

                AdminsRepository adminsRepo = new AdminsRepository(unitOfWork);
                Admin admin = await adminsRepo.GetById(adminFileDTO.AdminId);

                if (adminFileDTO != null)
                {
                    if (admin != null)
                    {
                        if (adminFileDTO.Id == 0)
                        {
                            adminFile = new AdminFile
                            {
                                AdminId = adminFileDTO.AdminId,
                                IsProfilePhoto = adminFile.IsProfilePhoto,
                                FileName = adminFileDTO.FileName,
                                FileType = adminFile.FileType,
                            };
                        }
                        else
                        {
                            adminFile = new AdminFile
                            {
                                Id = adminFileDTO.Id,
                                AdminId = adminFileDTO.AdminId,
                                IsProfilePhoto = adminFile.IsProfilePhoto,
                                FileName = adminFileDTO.FileName,
                                FileType = adminFile.FileType,
                            };
                        }
                    }

                    await adminFilesRepo.Save(adminFile);
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
            using (AdminsUnitOfWork unitOfWork = new AdminsUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                AdminFilesRepository adminFilesRepo = new AdminFilesRepository(unitOfWork);

                AdminFile adminFile = await adminFilesRepo.GetById(id);

                if (adminFile != null)
                {
                    await adminFilesRepo.Delete(adminFile);
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

