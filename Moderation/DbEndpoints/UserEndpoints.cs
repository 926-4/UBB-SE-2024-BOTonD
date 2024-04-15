using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.DbEndpoints
{
    public class User
    {
        public Guid Id { get; set; }
        public string Role { get; set; }

        public int PostScore {  get; set; }
        public int MarketPlaceScore {  get; set; }
        public string Status { get; set; }


    }
    public class UserEndpoints
    {
        private static string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO GroupUser VALUES (@Id,@Role,@Post,@Mp,@Status)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Role", user.Role);
                    command.Parameters.AddWithValue("@Post", user.PostScore);
                    command.Parameters.AddWithValue("@Mp", user.MarketPlaceScore);
                    command.Parameters.AddWithValue("@Status", user.Status);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static List<User> ReadUser()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM GroupUser";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                Id = reader.GetGuid(0),
                                Role = reader.GetString(1),
                                PostScore = reader.GetInt32(2),
                                MarketPlaceScore = reader.GetInt32(3),
                                Status = reader.GetString(4)
                            };
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }
        public static void UpdateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE GroupUser SET roleId=@Role, userPostScore=@post, userMarketplaceScore=@mp, status=@status WHERE userId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Role", user.Role);
                    command.Parameters.AddWithValue("@post", user.PostScore);
                    command.Parameters.AddWithValue("@mp", user.MarketPlaceScore);
                    command.Parameters.AddWithValue("@status", user.Status);
                    command.Parameters.AddWithValue("@id", user.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteRole(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM GroupUser WHERE userId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", user.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
