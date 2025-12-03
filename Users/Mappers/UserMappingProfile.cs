using AutoMapper;
using Notes_Api.Users.Dtos;
using Notes_Api.Users.Model;

namespace Notes_Api.Users.Mappers
{
    public class UserMappingProfile:Profile
    {

        public UserMappingProfile()
        {


            CreateMap<UserRequest, User>();

            CreateMap<User, UserResponse>()
                .ForMember(dest=>dest.Notes)


            


        }








    }
}
