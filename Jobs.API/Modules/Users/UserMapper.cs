using Jobs.API.Modules.Users.Models;

namespace Jobs.API.Modules.Users
{
    public static class UserMapper
    {
        public static User ToUser(this UserRegisterRequest r)
        {
            return new User
            {
                Email = r.Email,
                UserName = r.Email
            };
        }

        public static UserRegisterResponse ToUserRegisterResponse(this User u)
        {
            return new UserRegisterResponse(u.Id, u.Email);
        }
    }
}
