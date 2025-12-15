using Notes_Api.Notes.Dtos;

namespace Notes_Api.Users.Services
{
    public interface IUserCommandService
    {

        Task<NoteResponse> createNoteAsync(int iduser, NoteRequest newnote);















    }
}
