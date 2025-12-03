using Notes_Api.Notes.Dtos;

namespace Notes_Api.Users.Services
{
    public interface IUserQueryService
    {

        Task<GetAllNotesDtos> getAllNotesByUserId(int iduser);

       


    }
}
