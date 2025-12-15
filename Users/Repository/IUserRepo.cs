using Notes_Api.Notes.Dtos;
using Notes_Api.Users.Dtos;

namespace Notes_Api.Users.Repository
{
    public interface IUserRepo
    {
        Task<UserResponse> getUserByIdAsync(int iduser);


        Task<GetAllUsersDto > getAllUsersAsync();



        Task<NoteResponse> createNoteAsync(int iduser, NoteRequest newnote);

        //Task<NoteResponse> updateNoteAsync(int iduser, int noteId);


        //Task<NoteResponse> deleteNoteAsync(int userId, int noteid);

        Task<GetAllNotesDtos> getAllNotesByUserId(int iduser);







    }
}
