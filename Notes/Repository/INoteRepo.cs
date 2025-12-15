using Notes_Api.Notes.Dtos;

namespace Notes_Api.Notes.Repository
{
    public interface INoteRepo
    {
        Task<GetAllNotesDtos> getAllNotesByUserId(int iduser);

        Task<NoteResponse?> getNoteById(int userid, int noteid);

        Task<NoteResponse> createNoteAsync(int iduser, NoteRequest request);

        Task<NoteResponse?> updateNoteAsync(int iduser, int noteId, NoteRequest request);

        Task<bool> deleteNoteAsync(int userId, int noteid);
    }
}
