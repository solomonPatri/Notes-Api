using Notes_Api.Notes.Dtos;
using Notes_Api.Users.Repository;
using Notes_Api.Notes.Exceptions;
using Notes_Api.Users.Exceptions;
using Notes_Api.Users.Dtos;
using Notes_Api.System;

namespace Notes_Api.Users.Services
{
    public class UserQueryService : IUserQueryService
    {

        public readonly IUserRepo _repo;

        public UserQueryService(IUserRepo repo)
        {
            _repo = repo;

        }


        public async Task<UserResponse> getUserByIdAsync(int iduser)
        {

            UserResponse response = await _repo.getUserByIdAsync(iduser);

            if (response != null)
            {
                return response;

            }
            throw new UserNotFoundException();



        }

        public async Task<GetAllUsersDto> getAllUsersAsync()
        {

            GetAllUsersDto response = await _repo.getAllUsersAsync();

            if (response != null)
            {

                return response;


            }

            throw new UserNotFoundException();


       }

        public async Task<GetAllNotesDtos> getAllNotesByUserId(int iduser)
        {

            GetAllNotesDtos response = await _repo.getAllNotesByUserId(iduser);

            if (response == null || response.NotesList == null || response.NotesList.Count == 0)
            {
                throw new NotesNotFoundListExceptions();
            }

            return response;



        }














      
    }
}