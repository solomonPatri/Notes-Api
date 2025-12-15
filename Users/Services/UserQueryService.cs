using Notes_Api.Users.Dtos;
using Notes_Api.Users.Exceptions;
using Notes_Api.Users.Repository;

namespace Notes_Api.Users.Services
{
    public class UserQueryService : IUserQueryService
    {
        private readonly IUserRepo _repo;

        public UserQueryService(IUserRepo repo)
        {
            _repo = repo;
        }

        public async Task<GetAllUsersDto> GetAllUsersAsync()
        {
            var users = await _repo.GetAllUsersAsync();

            if (users.UserList == null || users.UserList.Count == 0)
            {
                throw new UsersNotFoundException();
            }

            return users;
        }

        public async Task<UserResponse> GetUserByIdAsync(int userId)
        {
            var user = await _repo.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }
    }
}
