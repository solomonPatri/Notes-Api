using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notes_Api.Notes.Dtos;
using Notes_Api.Notes.Exceptions;
using Notes_Api.Notes.Services;
using Notes_Api.Users.Dtos;
using Notes_Api.Users.Exceptions;
using Notes_Api.Users.Services;

namespace Notes_Api.Users
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserQueryService _userQueryService;
        private readonly IUserCommandService _userCommandService;
        private readonly INoteQueryService _noteQueryService;
        private readonly INoteCommandService _noteCommandService;

        public UserController(
            IUserQueryService userQueryService,
            IUserCommandService userCommandService,
            INoteQueryService noteQueryService,
            INoteCommandService noteCommandService)
        {
            _userQueryService = userQueryService;
            _userCommandService = userCommandService;
            _noteQueryService = noteQueryService;
            _noteCommandService = noteCommandService;
        }

        [HttpGet("allusers")]
        public async Task<ActionResult<GetAllUsersDto>> GetAllUsers()
        {
            try
            {
                var users = await _userQueryService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (UsersNotFoundException unf)
            {
                return NotFound(unf.Message);
            }
        }

        [HttpGet("getuserbyid/{iduser:int}")]
        public async Task<ActionResult<UserResponse>> GetUserById(int iduser)
        {
            try
            {
                var user = await _userQueryService.GetUserByIdAsync(iduser);
                return Ok(user);
            }
            catch (UserNotFoundException uf)
            {
                return NotFound(uf.Message);
            }
        }

        [HttpPost("addUser")]
        public async Task<ActionResult<UserResponse>> CreateUser([FromBody] UserRequest request)
        {
            try
            {
                var user = await _userCommandService.CreateUserAsync(request);
                return CreatedAtAction(nameof(GetUserById), new { iduser = user.Id }, user);
            }
            catch (UserAlreadyExistsException ua)
            {
                return Conflict(ua.Message);
            }
        }

        [HttpPut("updateUser/{iduser:int}")]
        public async Task<ActionResult<UserResponse>> UpdateUser(int iduser, [FromBody] UserRequest request)
        {
            try
            {
                var user = await _userCommandService.UpdateUserAsync(iduser, request);
                return Ok(user);
            }
            catch (UserAlreadyExistsException ua)
            {
                return Conflict(ua.Message);
            }
            catch (UserNotFoundException uf)
            {
                return NotFound(uf.Message);
            }
        }

        [HttpDelete("deleteuser/{iduser:int}")]
        public async Task<IActionResult> DeleteUser(int iduser)
        {
            try
            {
                await _userCommandService.DeleteUserAsync(iduser);
                return NoContent();
            }
            catch (UserNotFoundException uf)
            {
                return NotFound(uf.Message);
            }
        }

        [HttpGet("getallNotesbyuserid/{iduser:int}/notes")]
        public async Task<ActionResult<GetAllNotesDtos>> GetAllNotesByUserId(int iduser)
        {
            try
            {
                var notes = await _noteQueryService.getAllNotesByUserId(iduser);
                return Ok(notes);
            }
            catch (NotesNotFoundListExceptions nf)
            {
                return NotFound(nf.Message);
            }
            catch (UserNotFoundException uf)
            {
                return NotFound(uf.Message);
            }
        }

        [HttpGet("getnotebyid/{iduser:int}/notes/{noteId:int}", Name = nameof(GetNoteById))]
        public async Task<ActionResult<NoteResponse>> GetNoteById(int iduser, int noteId)
        {
            try
            {
                var note = await _noteQueryService.getNoteById(iduser, noteId);
                return Ok(note);
            }
            catch (NoteNotFoundException nf)
            {
                return NotFound(nf.Message);
            }
            catch (UserNotFoundException uf)
            {
                return NotFound(uf.Message);
            }
        }

        [HttpPost("addNote/{iduser:int}/notes")]
        public async Task<ActionResult<NoteResponse>> CreateNote(int iduser, [FromBody] NoteRequest request)
        {
            try
            {
                var note = await _noteCommandService.CreateNoteAsync(iduser, request);
                return CreatedAtAction(nameof(GetNoteById), new { iduser, noteId = note.Id }, note);
            }
            catch (NotesAlreadyExistException ae)
            {
                return Conflict(ae.Message);
            }
            catch (UserNotFoundException uf)
            {
                return NotFound(uf.Message);
            }
        }

        [HttpPut("updateNote/{iduser:int}/notes/{noteId:int}")]
        public async Task<ActionResult<NoteResponse>> UpdateNote(int iduser, int noteId, [FromBody] NoteRequest request)
        {
            try
            {
                var note = await _noteCommandService.UpdateNoteAsync(iduser, noteId, request);
                return Ok(note);
            }
            catch (NotesAlreadyExistException ae)
            {
                return Conflict(ae.Message);
            }
            catch (NoteNotFoundException nf)
            {
                return NotFound(nf.Message);
            }
            catch (UserNotFoundException uf)
            {
                return NotFound(uf.Message);
            }
        }

        [HttpDelete("deleteNote/{iduser:int}/notes/{noteId:int}")]
        public async Task<IActionResult> DeleteNote(int iduser, int noteId)
        {
            try
            {
                await _noteCommandService.DeleteNoteAsync(iduser, noteId);
                return NoContent();
            }
            catch (NoteNotFoundException nf)
            {
                return NotFound(nf.Message);
            }
            catch (UserNotFoundException uf)
            {
                return NotFound(uf.Message);
            }
        }
    }
}
