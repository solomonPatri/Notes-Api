using Notes_Api.Notes.Dtos;
using Notes_Api.Notes.Exceptions;
using Notes_Api.Notes.Repository;
using Notes_Api.Users.Exceptions;
using Notes_Api.Users.Repository;

namespace Notes_Api.Notes.Services
{
    public class NoteQueryService : INoteQueryService
    {
        private readonly IUserRepo _userRepo;
        private readonly INoteRepo _noteRepo;

        public NoteQueryService(IUserRepo userRepo, INoteRepo noteRepo)
        {
            _userRepo = userRepo;
            _noteRepo = noteRepo;
        }

        public async Task<GetAllNotesDtos> getAllNotesByUserId(int iduser)
        {
            await EnsureUserExists(iduser);

            var response = await _noteRepo.getAllNotesByUserId(iduser);

            if (response.NotesList == null || response.NotesList.Count == 0)
            {
                throw new NotesNotFoundListExceptions();
            }

            return response;
        }

        public async Task<NoteResponse> getNoteById(int iduser, int noteid)
        {
            await EnsureUserExists(iduser);

            var note = await _noteRepo.getNoteById(iduser, noteid);

            if (note == null)
            {
                throw new NoteNotFoundException();
            }

            return note;
        }

        private async Task EnsureUserExists(int userId)
        {
            var exists = await _userRepo.UserExistsAsync(userId);

            if (!exists)
            {
                throw new UserNotFoundException();
            }
        }
    }
}
