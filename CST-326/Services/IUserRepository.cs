using CST_326.Models;

namespace CST_326.DAO
{
    public interface IUserRepository<T>
    {
        //CRUD Commands
         User GetUser(T user);
        User EditUser(T user);
    }
}
