using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.DbEndpoints
{
    public class BannedUser
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public DateTime Start
        {
            get; set;
        }
        public DateTime End { get; set; }
    }
    public class BannedUserEndpoints
    {
        private static string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateBannedUser(BannedUser bannedUser)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO BannedUser VALUES (@Id,@u,@m,@s,@e)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", bannedUser.Id);
                    command.Parameters.AddWithValue("@u", bannedUser.UserId);
                    command.Parameters.AddWithValue("@m", bannedUser.Message);
                    command.Parameters.AddWithValue("@s", bannedUser.Start);
                    command.Parameters.AddWithValue("@e", bannedUser.End);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static List<BannedUser> ReadBannedUser()
        {
            List<BannedUser> bannedUsers = new List<BannedUser>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM BannedUser";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BannedUser bannedUser = new BannedUser
                            {
                                Id = reader.GetGuid(0),
                                UserId = reader.GetGuid(1),
                                Message = reader.GetString(2),
                                Start = reader.GetDateTime(3),
                                End = reader.GetDateTime(4)
                            };
                            bannedUsers.Add(bannedUser);
                        }
                    }
                }
            }
            return bannedUsers;
        }
        public static void UpdateBannedUser(BannedUser bannedUser)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE BannedUser SET userId=@u, message=@m, startTime=@s, endTime=@e WHERE bannedId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@u", bannedUser.UserId);
                    command.Parameters.AddWithValue("@m", bannedUser.Message);
                    command.Parameters.AddWithValue("@s", bannedUser.Start);
                    command.Parameters.AddWithValue("@e", bannedUser.End);
                    command.Parameters.AddWithValue("@id", bannedUser.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteBannedUser(BannedUser bannedUser)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM BannedUser WHERE bannedId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", bannedUser.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
