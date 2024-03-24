using CST_326.DAO;
using CST_326.Models;
using CST_326.Models.ViewModel;
using MySql.Data.MySqlClient;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace CST_326.Services
{
    public class UserDAO 
    {
        string myConnectionString = "Server=jonahmysqlserver.mysql.database.azure.com;Database=capstone;User Id=joenuh;Password=Jonah124;SslMode=Preferred;";
        public User FindUser(LoginViewModel user)
        {

            string sqlStatement = "SELECT * FROM users WHERE Username = @USERNAME and Password = @PASSWORD";

            using (MySqlConnection connection = new MySqlConnection(myConnectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@USERNAME", user.UserName).Value = user.UserName;
                command.Parameters.AddWithValue("@PASSWORD", user.Password).Value = user.Password;

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        User a = new User()
                        {
                            UserId = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            Password = reader.GetString(2),
                            FirstName = reader.GetString(3),
                            LastName = reader.GetString(4),
                            PhoneNumber = reader.GetString(5),
                        };
                        return a;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return null;


        }

        // register a new user 
        public void RegisterUser(RegistrationViewModel newUser)
        {
            string sqlStatement = "INSERT INTO users (Username, Password, FirstName, LastName, PhoneNumber) VALUES (@USERNAME, @PASSWORD, @FIRSTNAME, @LASTNAME, @PHONENUMBER)";

            using (MySqlConnection connection = new MySqlConnection(myConnectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@USERNAME", newUser.UserName);
                command.Parameters.AddWithValue("@PASSWORD", newUser.Password);
                command.Parameters.AddWithValue("@FIRSTNAME", newUser.FirstName);
                command.Parameters.AddWithValue("@LASTNAME", newUser.LastName);
                command.Parameters.AddWithValue("@PHONENUMBER", newUser.PhoneNumber);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
        }

    }
}
