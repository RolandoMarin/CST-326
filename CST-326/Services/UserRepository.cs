using CST_326.Models;
using CST_326.Models.ViewModel;
using CST_326.Services;

namespace CST_326.DAO
{
    public class UserRepository : IUserRepository
    {
        UserDAO userDAO = new UserDAO();

        public bool CreateUser(User user)
        {
            return userDAO.RegisterUser(user);
        }

        public bool DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public User EditUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUser(User user)
        {

            return userDAO.FindUser(user);
        }

        public List<Account> GetAccounts(User user)
        {
            return userDAO.GetAccountsByUserId(user);
        }
    }
}
