using Notes_Api.Notes.Dtos;

namespace Notes_Api.Notes.Repository
{
    public interface INoteRepo
    {


        Task<NoteResponse> createNoteAsync(int iduser, NoteRequest newnote);





    }
}
