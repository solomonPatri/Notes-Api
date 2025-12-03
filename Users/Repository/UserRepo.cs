using AutoMapper;
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

        //public async task<noteresponse> getnotebyid(int userid, int noteid)
        //{



        //}

        //public async  task<noteresponse> createnoteasync(int iduser)
        //{


        //}

        //public async task<noteresponse> updatenoteasync(int iduser, int noteid)
        //{


        //}

        //public async task<noteresponse> deletenoteasync(int userid, int noteid)
        //{




        //}







    }
}
