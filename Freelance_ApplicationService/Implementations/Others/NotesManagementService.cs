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
    public class NotesManagementService : IBaseManagementService
    {
        public static async Task<List<NoteDTO>> GetAll()
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                NotesRepository NotesRepo = new NotesRepository(unitOfWork);
                List<Note> Notes = await NotesRepo.GetAll();

                List<NoteDTO> NotesDTO = new List<NoteDTO>();

                if (Notes != null)
                {
                    foreach (var item in Notes)
                    {
                        NotesDTO.Add(new NoteDTO
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            Content = item.Content,
                            Priority = item.Priority,
                        });
                    }

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return NotesDTO;
            }
        }

        public static async Task<NoteDTO> GetById(int id)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                NotesRepository NotesRepo = new NotesRepository(unitOfWork);
                NoteDTO NoteDTO = new NoteDTO();

                Note Note = await NotesRepo.GetById(id);

                if (Note != null)
                {
                    NoteDTO.Id = Note.Id;
                    NoteDTO.UserId = Note.UserId;
                    NoteDTO.Content = Note.Content;
                    NoteDTO.Priority = Note.Priority;

                    unitOfWork.Commit();
                }
                else
                {
                    unitOfWork.Rollback();
                }
                return NoteDTO;
            }
        }

        public static async Task Save(NoteDTO NoteDTO)
        {
            using (FreelanceUnitOfWork unitOfWork = new FreelanceUnitOfWork())
            {
                unitOfWork.BeginTransaction();

                NotesRepository NotesRepo = new NotesRepository(unitOfWork);
                Note Note = new Note();

                if (NoteDTO != null)
                {
                    if (NoteDTO.Id == 0)
                    {
                        Note = new Note
                        {
                            UserId = NoteDTO.UserId,
                            Content = NoteDTO.Content,
                            Priority = NoteDTO.Priority,
                        };
                    }
                    else
                    {
                        Note = new Note
                        {
                            Id = NoteDTO.Id,
                            UserId = NoteDTO.UserId,
                            Content = NoteDTO.Content,
                            Priority = NoteDTO.Priority,
                        };
                    }

                    await NotesRepo.Save(Note);
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

                NotesRepository NotesRepo = new NotesRepository(unitOfWork);
                Note Note = await NotesRepo.GetById(id);

                if (Note != null)
                {
                    await NotesRepo.Delete(Note);
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
