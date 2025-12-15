using Notes_Api.Notes.Dtos;
using Notes_Api.Notes.Exceptions;
using Notes_Api.Notes.Repository;
using Notes_Api.Users.Exceptions;
using Notes_Api.Users.Repository;

namespace Notes_Api.Notes.Services
{
    public class NoteCommandService : INoteCommandService
    {
        private readonly INoteRepo _noteRepo;
        private readonly IUserRepo _userRepo;

        public NoteCommandService(INoteRepo noteRepo, IUserRepo userRepo)
        {
            _noteRepo = noteRepo;
            _userRepo = userRepo;
        }

        public async Task<NoteResponse> CreateNoteAsync(int userId, NoteRequest request)
        {
            await EnsureUserExists(userId);
            return await _noteRepo.createNoteAsync(userId, request);
        }

        public async Task<NoteResponse> UpdateNoteAsync(int userId, int noteId, NoteRequest request)
        {
            await EnsureUserExists(userId);
            var note = await _noteRepo.updateNoteAsync(userId, noteId, request);

            if (note == null)
            {
                throw new NoteNotFoundException();
            }

            return note;
        }

        public async Task DeleteNoteAsync(int userId, int noteId)
        {
            await EnsureUserExists(userId);
            var deleted = await _noteRepo.deleteNoteAsync(userId, noteId);

            if (!deleted)
            {
                throw new NoteNotFoundException();
            }
        }

        private async Task EnsureUserExists(int userId)
        {
            if (!await _userRepo.UserExistsAsync(userId))
            {
                throw new UserNotFoundException();
            }
        }
    }
}
