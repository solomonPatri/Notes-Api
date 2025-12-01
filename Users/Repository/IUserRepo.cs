using Notes_Api.Notes.Dtos;

namespace Notes_Api.Users.Repository
{
    public interface IUserRepo
    {

        Task<GetAllNotesDtos> GetAllNotesByUserId(int iduser);













    }
}
