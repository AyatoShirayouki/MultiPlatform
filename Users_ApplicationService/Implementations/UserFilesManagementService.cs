using Base.ManagementService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_ApplicationService.DTOs;
using Users_Data.Entities;
using Users_Repository.Implementations;
using Users_Repository.Implementations.EntityRepositories;

namespace Users_ApplicationService.Implementations
{
    public class UserFilesManagementService : IBaseManagementService
    {
        public static async Task<List<UserFileDTO>> GetAll()
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                UserFilesRepository userFilesRepo = new UserFilesRepository(unitOfWork);
                List<UserFile> userFiles = await userFilesRepo.GetAll();

                List<UserFileDTO> userFilesDTO = new List<UserFileDTO>();

                if (userFiles != null)
                {
                    foreach (var item in userFiles)
                    {
                        userFilesDTO.Add(new UserFileDTO
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            IsProfilePhoto = item.IsProfilePhoto,
                            IsCoverPhoto = item.IsCoverPhoto,
                            IsCV = item.IsCV,
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
                return userFilesDTO;
            }
        }

        public static async Task<List<UserFileDTO>> GetFilesByParentId(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                UsersRepository usersRepo = new UsersRepository(unitOfWork);
                UserFilesRepository userFilesRepo = new UserFilesRepository(unitOfWork);

                List<UserFileDTO> userFilesDTO = new List<UserFileDTO>();

                User user = await usersRepo.GetById(id);

                if (user != null)
                {
                    List<UserFile> userFiles = await userFilesRepo.GetAll();

                    foreach (var item in userFiles)
                    {
                        if (item.UserId == user.Id)
                        {
                            userFilesDTO.Add(new UserFileDTO
                            {
                                Id = item.Id,
                                UserId = item.UserId,
                                IsProfilePhoto = item.IsProfilePhoto,
                                IsCoverPhoto = item.IsCoverPhoto,
                                IsCV = item.IsCV,
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
                return userFilesDTO;
            }
        }

        public static async Task<UserFileDTO> GetById(int id)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                UserFilesRepository userFilesRepo = new UserFilesRepository(unitOfWork);
                UserFileDTO userFileDTO = new UserFileDTO();

                UserFile userFile = await userFilesRepo.GetById(id);

                if (userFile != null)
                {
                    userFileDTO.Id = userFile.Id;
                    userFileDTO.IsProfilePhoto = userFile.IsProfilePhoto;
                    userFileDTO.IsCoverPhoto = userFile.IsCoverPhoto;
                    userFileDTO.IsCV = userFile.IsCV;
                    userFileDTO.FileName = userFile.FileName;
                    userFileDTO.FileType = userFile.FileType;
                    userFileDTO.UserId = userFile.UserId;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return userFileDTO;
            }
        }

        public static async Task Save(UserFileDTO userFileDTO)
        {
            using (UsersUnitOfWork unitOfWork = new UsersUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                UserFilesRepository userFilesRepo = new UserFilesRepository(unitOfWork);
                UserFile userFile = new UserFile();

                if (userFileDTO != null)
                {
                    UsersRepository usersRepo = new UsersRepository(unitOfWork);
                    User user = await usersRepo.GetById(userFileDTO.UserId);

                    if (user != null)
                    {
                        if (userFileDTO.Id == 0)
                        {
                            userFile = new UserFile
                            {
                                UserId = userFileDTO.UserId,
                                IsProfilePhoto = userFileDTO.IsProfilePhoto,
                                IsCoverPhoto = userFileDTO.IsCoverPhoto,
                                IsCV = userFileDTO.IsCV,
                                FileName = userFileDTO.FileName,
                                FileType = userFileDTO.FileType,
                            };
                        }
                        else
                        {
                            userFile = new UserFile
                            {
                                Id = userFileDTO.Id,
                                UserId = userFileDTO.UserId,
                                IsProfilePhoto = userFileDTO.IsProfilePhoto,
                                IsCoverPhoto = userFileDTO.IsCoverPhoto,
                                IsCV = userFileDTO.IsCV,
                                FileName = userFileDTO.FileName,
                                FileType = userFileDTO.FileType,
                            };
                        }
                    }

                    await userFilesRepo.Save(userFile);
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

                UserFilesRepository userFilesRepo = new UserFilesRepository(unitOfWork);

                UserFile userFile = await userFilesRepo.GetById(id);

                if (userFile != null)
                {
                    await userFilesRepo.Delete(userFile);
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
