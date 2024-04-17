using Moderation.Entities;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.DbEndpoints
{
    public class UserEndpoints
    {
        private static readonly string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    
        public static void CreateUser(User user)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "INSERT INTO GroupUser (Id, Username, PostScore, MarketplaceScore, StatusRestriction, StatusRestrictionDate, StatusMessage) " +
                         "VALUES (@Id, @Username, @PostScore, @MarketplaceScore, @StatusRestriction, @StatusRestrictionDate, @StatusMessage)";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@PostScore", user.PostScore);
            command.Parameters.AddWithValue("@MarketplaceScore", user.MarketplaceScore);
            command.Parameters.AddWithValue("@StatusRestriction", (int)user.Status.Restriction);
            command.Parameters.AddWithValue("@StatusRestrictionDate", user.Status.RestrictionDate);
            command.Parameters.AddWithValue("@StatusMessage", user.Status.Message);

            command.ExecuteNonQuery();
        }
        public static List<User> ReadAllUsers()
        {
            List<User> users = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string sql = "SELECT Id, Username, PostScore, MarketplaceScore, StatusRestriction, StatusRestrictionDate, StatusMessage FROM GroupUser";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserRestriction restriction = (UserRestriction)reader.GetInt32(4);
                    UserStatus status = new(restriction, reader.GetDateTime(5), reader.GetString(6));

                    User user = new(reader.GetGuid(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), status);
                    users.Add(user);
                }
            }

            return users;
        }
        public static void UpdateUser(User user)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "UPDATE GroupUser SET Username = @Username, PostScore = @PostScore, MarketplaceScore = @MarketplaceScore, " +
                         "StatusRestriction = @StatusRestriction, StatusRestrictionDate = @StatusRestrictionDate, StatusMessage = @StatusMessage " +
                         "WHERE Id = @Id";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@PostScore", user.PostScore);
            command.Parameters.AddWithValue("@MarketplaceScore", user.MarketplaceScore);
            command.Parameters.AddWithValue("@StatusRestriction", (int)user.Status.Restriction);
            command.Parameters.AddWithValue("@StatusRestrictionDate", user.Status.RestrictionDate);
            command.Parameters.AddWithValue("@StatusMessage", user.Status.Message);
            command.Parameters.AddWithValue("@Id", user.Id);

            command.ExecuteNonQuery();
        }
        public static void DeleteUser(Guid userId)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "DELETE FROM GroupUser WHERE Id = @Id";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", userId);

            command.ExecuteNonQuery();
        }
    }
}
