using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notes_Api.Data;
using Notes_Api.Users.Dtos;
using Notes_Api.Users.Model;

namespace Notes_Api.Users.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserRepo(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<bool> UserExistsAsync(int userId)
        {
            return _context.Users.AnyAsync(u => u.Id == userId);
        }

        public async Task<GetAllUsersDto> GetAllUsersAsync()
        {
            var users = await _context.Users
                .AsNoTracking()
                .Include(u => u.Notes)
                    .ThenInclude(n => n.NoteCategories)
                .ToListAsync();

            return new GetAllUsersDto
            {
                UserList = _mapper.Map<List<UserResponse>>(users ?? new List<User>())
            };
        }

        public async Task<UserResponse?> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Include(u => u.Notes)
                    .ThenInclude(n => n.NoteCategories)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user == null ? null : _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest request)
        {
            var user = _mapper.Map<User>(request);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await _context.Entry(user)
                .Collection(u => u.Notes)
                .LoadAsync();

            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse?> UpdateUserAsync(int userId, UserRequest request)
        {
            var user = await _context.Users
                .Include(u => u.Notes)
                    .ThenInclude(n => n.NoteCategories)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return null;
            }

            user.Email = request.Email;
            user.Password = request.Password;
            user.Age = request.Age;

            await _context.SaveChangesAsync();

            return _mapper.Map<UserResponse>(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<bool> EmailExistsAsync(string email, int? excludeUserId = null)
        {
            return _context.Users.AnyAsync(u => u.Email == email && (!excludeUserId.HasValue || u.Id != excludeUserId.Value));
        }
    }
}
