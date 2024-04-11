using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.OnlineId;

namespace Moderation.DbEndpoints
{
    public class MutedUser
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
    public class MutedUserEndpoints
    {
        private static string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateMutedUser(MutedUser mutedUser)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO MutedUser VALUES (@Id,@u,@m,@s,@e)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", mutedUser.Id);
                    command.Parameters.AddWithValue("@u", mutedUser.UserId);
                    command.Parameters.AddWithValue("@m", mutedUser.Message);
                    command.Parameters.AddWithValue("@s", mutedUser.Start);
                    command.Parameters.AddWithValue("@e", mutedUser.End);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static List<MutedUser> ReadMutedUser()
        {
            List<MutedUser> mutedUsers = new List<MutedUser>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM MutedUser";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MutedUser mutedUser = new MutedUser
                            {
                                Id = reader.GetGuid(0),
                                UserId = reader.GetGuid(1),
                                Message = reader.GetString(2),
                                Start = reader.GetDateTime(3),
                                End = reader.GetDateTime(4)
                            };
                            mutedUsers.Add(mutedUser);
                        }
                    }
                }
            }
            return mutedUsers;
        }
        public static void UpdateMutedUser(MutedUser mutedUser)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE MutedUser SET userId=@u, message=@m, startTime=@s, endTime=@e WHERE mutedId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@u", mutedUser.UserId);
                    command.Parameters.AddWithValue("@m", mutedUser.Message);
                    command.Parameters.AddWithValue("@s", mutedUser.Start);
                    command.Parameters.AddWithValue("@e", mutedUser.End);
                    command.Parameters.AddWithValue("@id", mutedUser.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteMutedUser(MutedUser mutedUser)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM MutedUser WHERE mutedId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", mutedUser.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
