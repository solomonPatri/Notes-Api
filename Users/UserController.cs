using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using Notes_Api.Notes.Dtos;
using Notes_Api.Notes.Exceptions;
using Notes_Api.Users.Services;
using Notes_Api.Users.Repository;

namespace Notes_Api.Users
{
    [ApiController]
    [Route("api/v1/[Controller]")]


    public class UserController:ControllerBase
    {

        public readonly IUserRepo _repo;


        public UserController(IUserRepo repo)
        {

            _repo = repo;

        }


        [HttpGet("allnotes")]


        public async Task<ActionResult<GetAllNotesDtos>> getAllNotesByUserId(int iduser)
        {

            try
            {
                GetAllNotesDtos note = await _repo.getAllNotesByUserId(iduser);

                return Ok(note);
            }
            catch (NotesNotFoundListExceptions nf)
            {

                return NotFound(nf.Message);
            }
        
        
        }




    }
}
