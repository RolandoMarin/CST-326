using CST_326.DAO;
using CST_326.Models;
using CST_326.Models.ViewModel;
using MySql.Data.MySqlClient;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;



namespace CST_326.Services
{
    public class UserDAO 
    {
        string myConnectionString = "Server=jonahmysqlserver.mysql.database.azure.com;Database=capstone;User Id=joenuh;Password=Jonah124;SslMode=Preferred;";

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public User FindUser(LoginViewModel user)
        {
            string sqlStatement = "SELECT * FROM users WHERE Username = @USERNAME";

            using (MySqlConnection connection = new MySqlConnection(myConnectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@USERNAME", user.UserName);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string hashedPassword = reader.GetString(2); // Assuming Password is at index 2
                        string salt = reader.GetString(6); // Assuming Salt is at index 6

                        if (hashedPassword == HashPassword(user.Password, salt))
                        {
                            // Passwords match, return user
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
                        else
                        {
                            // Passwords don't match
                            return null;
                        }
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

        public void RegisterUser(RegistrationViewModel newUser)
        {
            string salt = GenerateSalt();
            string hashedPassword = HashPassword(newUser.Password, salt);

            string sqlStatement = "INSERT INTO users (Username, Password, Salt, FirstName, LastName, PhoneNumber) VALUES (@USERNAME, @PASSWORD, @SALT, @FIRSTNAME, @LASTNAME, @PHONENUMBER)";

            using (MySqlConnection connection = new MySqlConnection(myConnectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@USERNAME", newUser.UserName);
                command.Parameters.AddWithValue("@PASSWORD", hashedPassword);
                command.Parameters.AddWithValue("@SALT", salt);
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

        private string GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
    }
}
