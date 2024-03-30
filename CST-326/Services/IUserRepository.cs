using CST_326.Models;

namespace CST_326.DAO
{
    public interface IUserRepository
    {
        //CRUD Commands
         User GetUser(User username);
         User EditUser(User user);
         bool CreateUser(User user);
         bool DeleteUser(User user);

    }
}
