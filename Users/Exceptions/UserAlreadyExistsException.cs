using Notes_Api.System;

namespace Notes_Api.Users.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException() : base(ExceptionsMessage.UserAlreadyExistsException)
        {
        }
    }
}
