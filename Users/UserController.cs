using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notes_Api.Notes.Dtos;
using Notes_Api.Notes.Exceptions;
using Notes_Api.Users.Exceptions;
using Notes_Api.Users.Services;
using Notes_Api.Users.Dtos;

namespace Notes_Api.Users
{
    [ApiController]
    [Route("api/v1/[Controller]")]


    public class UserController:ControllerBase
    {

        public readonly IUserQueryService _queryService;


        public UserController(IUserQueryService queryService)
        {

            _queryService = queryService;

        }

        [HttpGet("getuserbyId/{userid:int}")]

        public async Task<ActionResult<UserResponse>> getUserByIdAsync(int userid)
        {

            try
            {

                UserResponse response = await _queryService.getUserByIdAsync(userid);

                return Ok(response);



            }catch(UserNotFoundException nf)
            {

                return NotFound(nf.Message);
            }



        }


        [HttpGet("getAllUsers")]


        public async Task<ActionResult<GetAllUsersDto>> getAllUsersAsync()
        {
            try
            {
                GetAllUsersDto response = await _queryService.getAllUsersAsync();

                return Ok(response);



            }
            catch (UserNotFoundException nf)
            {

                return NotFound(nf.Message);
            }



        }









        [HttpGet("{iduser:int}/notes")]


        public async Task<ActionResult<GetAllNotesDtos>> getAllNotesByUserId(int iduser)
        {

            try
            {
                GetAllNotesDtos note = await _queryService.getAllNotesByUserId(iduser);

                return Ok(note);
            }
            catch (NotesNotFoundListExceptions nf)
            {

                return NotFound(nf.Message);
            }
        
        
        }
       


      


    }
}
