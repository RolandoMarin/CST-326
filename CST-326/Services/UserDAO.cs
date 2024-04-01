using CST_326.DAO;
using CST_326.Models;
using CST_326.Models.ViewModel;
using MySql.Data.MySqlClient;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace CST_326.Services
{
    public class UserDAO 
    {
        string myConnectionString = "Server=jonahmysqlserver.mysql.database.azure.com;Database=capstone;User Id=joenuh;Password=Jonah124;SslMode=Preferred;";
        public User FindUser(User user)
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
                            EmailAddress = reader.GetString(6),
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

        public bool DeleteUser(User user)
        {
            string deleteQuery = "DELETE FROM users WHERE UserId = @Id";

            using (MySqlConnection connection = new MySqlConnection(myConnectionString))
            {
                MySqlCommand command = new MySqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@id", user.UserId);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // register a new user 
        public bool RegisterUser(User newUser)
        {
            string sqlStatement = "INSERT INTO users (Username, Password, FirstName, LastName,Phone,Email) VALUES (@USERNAME,@PASSWORD,@FIRSTNAME,@LASTNAME,@PHONE,@EMAIL)";

            using (MySqlConnection connection = new MySqlConnection(myConnectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@USERNAME", newUser.UserName);
                command.Parameters.AddWithValue("@PASSWORD", newUser.Password);
                command.Parameters.AddWithValue("@FIRSTNAME", newUser.FirstName);
                command.Parameters.AddWithValue("@LASTNAME", newUser.LastName);
                command.Parameters.AddWithValue("@PHONE", newUser.PhoneNumber);
                command.Parameters.AddWithValue("@EMAIL", newUser.EmailAddress);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return false;
        }

        public List<Account> GetAccountsByUserId(User user)
        {
            List<Account> accounts = new List<Account>();

            string sqlStatement = "SELECT * FROM accounts WHERE UserId = @UserId";

            using (MySqlConnection connection = new MySqlConnection(myConnectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@UserId", user.UserId);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Account account = new Account()
                        {
                            // Populate account properties from the database
                            AccountId = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            AccountNumber = reader.GetString(2),
                            AccountType = reader.GetString(3),
                            Balance = reader.GetInt32(4),
                            CreatedAt = reader.GetDateTime(5)
                        };
                        accounts.Add(account);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return accounts;
        }
    }
}
