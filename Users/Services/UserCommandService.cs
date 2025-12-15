using Notes_Api.Notes.Dtos;
using Notes_Api.Users.Repository;

namespace Notes_Api.Users.Services
{
    public class UserCommandService:IUserCommandService
    {

        public readonly IUserRepo _repo;

        public UserCommandService(IUserRepo repo)
        {
            _repo = repo;

        }



        public async Task<NoteResponse> createNoteAsync(int iduser, NoteRequest newnote)
        {







        }












    }
}
