
using Notes_Api.System;
namespace Notes_Api.Notes.Exceptions
{
    public class NoteNotFoundException:Exception
    {

        public NoteNotFoundException() : base(ExceptionsMessage.NotesNotFoundException)
        {

        }
    }
}
