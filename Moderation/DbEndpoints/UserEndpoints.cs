using Microsoft.Data.SqlClient;
using Moderation.Entities;
using System.Configuration;

namespace Moderation.DbEndpoints
{
    public class UserEndpoints
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public static void CreateUser(User user)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "INSERT INTO [User] (Id, Username, Password) " +
                         "VALUES (@Id, @Username, @Password)";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);

            command.ExecuteNonQuery();
        }
        public static List<User> ReadAllUsers()
        {
            List<User> users = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string sql = "SELECT Id, Username, Password FROM [User]";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    User user = new(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
                    users.Add(user);
                }
            }

            return users;
        }
        public static void UpdateUser(User user)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "UPDATE User" +
                         "SET Username = @Username, Password = @Password" +
                         "WHERE Id = @Id";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Id", user.Id);

            command.ExecuteNonQuery();
        }
        public static void DeleteUser(Guid id)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "DELETE FROM User WHERE Id = @Id";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }
}