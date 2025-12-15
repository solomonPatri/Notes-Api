using Notes_Api.Users.Dtos;
using Notes_Api.Users.Exceptions;
using Notes_Api.Users.Repository;

namespace Notes_Api.Users.Services
{
    public class UserCommandService : IUserCommandService
    {
        private readonly IUserRepo _repo;

        public UserCommandService(IUserRepo repo)
        {
            _repo = repo;
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest request)
        {
            var emailExists = await _repo.EmailExistsAsync(request.Email);

            if (emailExists)
            {
                throw new UserAlreadyExistsException();
            }

            return await _repo.CreateUserAsync(request);
        }

        public async Task<UserResponse> UpdateUserAsync(int userId, UserRequest request)
        {
            var exists = await _repo.UserExistsAsync(userId);

            if (!exists)
            {
                throw new UserNotFoundException();
            }

            var emailExists = await _repo.EmailExistsAsync(request.Email, userId);

            if (emailExists)
            {
                throw new UserAlreadyExistsException();
            }

            var response = await _repo.UpdateUserAsync(userId, request);

            if (response == null)
            {
                throw new UserNotFoundException();
            }

            return response;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var deleted = await _repo.DeleteUserAsync(userId);

            if (!deleted)
            {
                throw new UserNotFoundException();
            }
        }
    }
}
