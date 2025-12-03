using Notes_Api.Notes.Dtos;
using Notes_Api.Users.Dtos;

namespace Notes_Api.Users.Repository
{
    public interface IUserRepo
    {

        Task<GetAllNotesDtos> getAllNotesByUserId(int iduser);

        //Task<NoteResponse> getNoteById(int userid, int noteid);

        //Task<NoteResponse> createNoteAsync(int iduser);

        //Task<NoteResponse> updateNoteAsync(int iduser, int noteId);

        //Task<NoteResponse> deleteNoteAsync(int userId, int noteid);









    }
}
