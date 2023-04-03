using Base.ManagementService;
using Freelance_ApplicationService.DTOs.Others;
using Freelance_Data.Entities.Others;
using Freelance_Repository.Implementations.EntityRepositories.Others;
using Freelance_Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_ApplicationService.Implementations.Others
{
	public class SkillsToUsersManagementService : IBaseManagementService
	{
		public static async Task<List<SkillToUserDTO>> GetAll()
		{
			using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
			{
				unitOfWork.BeginTransaction();

				SkillsToUsersRepository SkillToUsersRepo = new SkillsToUsersRepository(unitOfWork);
				List<SkillToUser> SkillToUsers = await SkillToUsersRepo.GetAll();

				List<SkillToUserDTO> SkillToUsersDTO = new List<SkillToUserDTO>();

				if (SkillToUsers != null)
				{
					foreach (var item in SkillToUsers)
					{
						SkillToUsersDTO.Add(new SkillToUserDTO
						{
							Id = item.Id,
							UserId = item.UserId,
							SkillId = item.SkillId
						});
					}

					unitOfWork.Commit();
				}
				else
				{
					unitOfWork.Rollback();
				}
				return SkillToUsersDTO;
			}
		}

		public static async Task<SkillToUserDTO> GetById(int id)
		{
			using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
			{
				unitOfWork.BeginTransaction();

				SkillsToUsersRepository SkillToUsersRepo = new SkillsToUsersRepository(unitOfWork);
				SkillToUserDTO SkillToUserDTO = new SkillToUserDTO();

				SkillToUser SkillToUser = await SkillToUsersRepo.GetById(id);

				if (SkillToUser != null)
				{
					SkillToUserDTO.Id = SkillToUser.Id;
					SkillToUserDTO.UserId = SkillToUser.UserId;
					SkillToUserDTO.SkillId = SkillToUser.SkillId;
					unitOfWork.Commit();
				}
				else
				{
					unitOfWork.Rollback();
				}
				return SkillToUserDTO;
			}
		}

		public static async Task Save(SkillToUserDTO SkillToUserDTO)
		{
			using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
			{
				unitOfWork.BeginTransaction();

				SkillsToUsersRepository SkillToUsersRepo = new SkillsToUsersRepository(unitOfWork);
				SkillToUser SkillToUser = new SkillToUser();

				if (SkillToUserDTO != null)
				{
					if (SkillToUserDTO.Id == 0)
					{
						SkillToUser = new SkillToUser
						{
							UserId = SkillToUserDTO.UserId,
							SkillId = SkillToUserDTO.SkillId
						};
					}
					else
					{
						SkillToUser = new SkillToUser
						{
							Id = SkillToUserDTO.Id,
							UserId = SkillToUserDTO.UserId,
							SkillId = SkillToUserDTO.SkillId
						};
					}

					await SkillToUsersRepo.Save(SkillToUser);
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
			using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
			{
				unitOfWork.BeginTransaction();

				SkillsToUsersRepository SkillToUsersRepo = new SkillsToUsersRepository(unitOfWork);
				SkillToUser SkillToUser = await SkillToUsersRepo.GetById(id);

				if (SkillToUser != null)
				{
					await SkillToUsersRepo.Delete(SkillToUser);
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
