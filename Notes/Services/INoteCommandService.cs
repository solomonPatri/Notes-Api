using Notes_Api.Notes.Dtos;

namespace Notes_Api.Notes.Services
{
    public interface INoteCommandService
    {
        Task<NoteResponse> CreateNoteAsync(int userId, NoteRequest request);

        Task<NoteResponse> UpdateNoteAsync(int userId, int noteId, NoteRequest request);

        Task DeleteNoteAsync(int userId, int noteId);
    }
}
