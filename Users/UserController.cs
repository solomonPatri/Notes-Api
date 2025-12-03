using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notes_Api.Notes.Dtos;
using Notes_Api.Notes.Exceptions;
using Notes_Api.Users.Services;

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


        [HttpGet("{iduser:int}/notes")]


        public async Task<ActionResult<GetAllNotesDtos>> GetAllNotesByUserId(int iduser)
        
        
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
