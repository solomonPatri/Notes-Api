using Notes_Api.Users.Dtos;

namespace Notes_Api.Users.Repository
{
    public interface IUserRepo
    {
        Task<bool> UserExistsAsync(int userId);

        Task<GetAllUsersDto> GetAllUsersAsync();

        Task<UserResponse?> GetUserByIdAsync(int userId);

        Task<UserResponse> CreateUserAsync(UserRequest request);

        Task<UserResponse?> UpdateUserAsync(int userId, UserRequest request);

        Task<bool> DeleteUserAsync(int userId);

        Task<bool> EmailExistsAsync(string email, int? excludeUserId = null);
    }
}
