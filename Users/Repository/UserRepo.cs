using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Notes_Api.Data;
using Notes_Api.Notes.Dtos;
using Notes_Api.Notes.Model;
using Notes_Api.Users.Dtos;
using Notes_Api.Users.Model;


namespace Notes_Api.Users.Repository
{
    public class UserRepo : IUserRepo
    {
        public readonly AppDbContext _context;

        public readonly IMapper _mapper;



        public UserRepo(AppDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;

        }

        public async Task<UserResponse> getUserByIdAsync(int iduser)
        {
            var user = await _context.Users
                .Include(n=>n.Notes)
                .FirstOrDefaultAsync(u=>u.Id == iduser);
                

            return _mapper.Map<UserResponse>(user);

        }


        public async Task<GetAllUsersDto> getAllUsersAsync()
        {

            var list = await _context.Users.ToListAsync();

            var mapped = _mapper.Map<List<UserResponse>>(list);

            return new GetAllUsersDto
            {

                UserList = mapped

            };

        }


        public async  Task<GetAllNotesDtos> getAllNotesByUserId(int iduser)
        {

            var searchuser = await _context.Users
                .AsNoTracking()
                .Include(u => u.Notes)
                    .ThenInclude(n => n.NoteCategories)
                .FirstOrDefaultAsync(u => u.Id == iduser);

            if (searchuser == null)
            {
                return null;
            }

            var mapped = _mapper.Map<List<NoteResponse>>(searchuser.Notes ?? new List<Note>());

            return new GetAllNotesDtos
            {
                NotesList = mapped

            };





        }




      public async  Task<NoteResponse> createNoteAsync(int iduser,NoteRequest newnote)
        {

            var listnotesidUser = await _context.Notes.Where(u => u.UserId == iduser).ToListAsync();

            var searched = _mapper.Map<Note>(newnote);

            listnotesidUser.Add(searched);
            await _context.SaveChangesAsync();

            NoteResponse response = _mapper.Map<NoteResponse>(searched);

            return response;



        }







    }
}
