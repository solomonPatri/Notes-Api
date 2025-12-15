using Notes_Api.Notes.Dtos;

namespace Notes_Api.Notes.Services
{
    public interface INoteQueryService
    {
        Task<GetAllNotesDtos> getAllNotesByUserId(int iduser);

        Task<NoteResponse> getNoteById(int iduser, int noteid);
    }
}
