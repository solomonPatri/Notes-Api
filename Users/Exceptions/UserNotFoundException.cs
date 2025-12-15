using Notes_Api.System;

namespace Notes_Api.Users.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base(ExceptionsMessage.UserNotFoundException)
        {
        }
    }
}
