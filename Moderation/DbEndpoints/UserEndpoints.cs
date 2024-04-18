using Moderation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Moderation.DbEndpoints
{
    public class UserEndpoints
    {
        private static readonly string connectionString =
            "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password=1234567!a;" +
            "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

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

                string sql = "SELECT Id, Username, Password FROM User";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    User user = new User(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
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