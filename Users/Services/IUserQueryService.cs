using Notes_Api.Notes.Dtos;
using Notes_Api.Users.Dtos;

namespace Notes_Api.Users.Services
{
    public interface IUserQueryService
    {
        Task<UserResponse> getUserByIdAsync(int iduser);

        Task<GetAllUsersDto> getAllUsersAsync();


        Task<GetAllNotesDtos> getAllNotesByUserId(int iduser);



        


    }
}
