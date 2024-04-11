using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.DbEndpoints
{
    public class JoinRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
    }
    public class JoinRequestEndpoints
    {
        private static string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateJoinRequest(JoinRequest joinRequest)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO JoinRequest VALUES (@Id,@u,@s)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", joinRequest.Id);
                    command.Parameters.AddWithValue("@u", joinRequest.UserId);
                    command.Parameters.AddWithValue("@s", joinRequest.Status);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static List<JoinRequest> ReadRequest()
        {
            List<JoinRequest> joinRequests = new List<JoinRequest>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM JoinRequest";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JoinRequest joinRequest = new JoinRequest
                            {
                                Id = reader.GetGuid(0),
                                UserId = reader.GetGuid(1),
                                Status = reader.GetString(2),
                            };
                            joinRequests.Add(joinRequest);
                        }
                    }
                }
            }
            return joinRequests;
        }
        public static void UpdateJoinRequest(JoinRequest joinRequest)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE JoinRequest SET userId=@u,status=@s WHERE joinId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@u", joinRequest.UserId);
                    command.Parameters.AddWithValue("@s", joinRequest.Status);
                    command.Parameters.AddWithValue("@id", joinRequest.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteJoinRequest(JoinRequest joinRequest)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM JoinRequest WHERE joinId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", joinRequest.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
