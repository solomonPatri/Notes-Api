using Notes_Api.Users.Dtos;

namespace Notes_Api.Users.Services
{
    public interface IUserCommandService
    {
        Task<UserResponse> CreateUserAsync(UserRequest request);

        Task<UserResponse> UpdateUserAsync(int userId, UserRequest request);

        Task DeleteUserAsync(int userId);
    }
}
