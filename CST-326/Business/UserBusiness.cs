using CST_326.Models;
using CST_326.Models.ViewModel;

namespace CST_326.Business
{
    public class UserBusiness
    {
        public User AddUser(RegisterViewModel registeredUser)
        {
            var user = new User
            {
                UserName = registeredUser.UserName,
                EmailAddress = registeredUser.Email,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                Password = registeredUser.Password,
                PhoneNumber = registeredUser.PhoneNumber,
            };
            return user;
        }
        public User GetUser(LoginViewModel registeredUser)
        {
            var user = new User
            {
                UserName = registeredUser.UserName,
                Password = registeredUser.Password,
            };
            return user;
        }
    }
}
