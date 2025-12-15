using Notes_Api.Users.Dtos;

namespace Notes_Api.Users.Services
{
    public interface IUserQueryService
    {
        Task<GetAllUsersDto> GetAllUsersAsync();

        Task<UserResponse> GetUserByIdAsync(int userId);
    }
}
