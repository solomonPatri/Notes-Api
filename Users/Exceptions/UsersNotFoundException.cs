using Notes_Api.System;

namespace Notes_Api.Users.Exceptions
{
    public class UsersNotFoundException : Exception
    {
        public UsersNotFoundException() : base(ExceptionsMessage.UsersNotFoundException)
        {
        }
    }
}
