using Moderation.Entities;
using Microsoft.Data.SqlClient;

namespace Moderation.DbEndpoints
{
    public class JoinRequestEndpoints
    {
        private static readonly string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateJoinRequest(JoinRequest joinRequest)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            // Insert JoinRequest entry
            string insertJoinRequestSql = "INSERT INTO JoinRequest (Id, UserId) VALUES (@Id, @UserId)";
            using (SqlCommand command = new(insertJoinRequestSql, connection))
            {
                command.Parameters.AddWithValue("@Id", joinRequest.Id);
                command.Parameters.AddWithValue("@UserId", joinRequest.userId);
                command.ExecuteNonQuery();
            }
        }
        public static List<JoinRequest> ReadAllJoinRequests()
        {
            List<JoinRequest> joinRequests = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string sql = "SELECT j.Id, j.UserId, m.[Key], m.[Value] " +
                             "FROM JoinRequest j ";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    JoinRequest joinRequest = new JoinRequest
                    (
                        reader.GetGuid(0),
                        reader.GetGuid(1)
                    );
                }
            }

            return joinRequests;
        }

        public static void DeleteJoinRequest(Guid joinRequestId)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();
            string deleteJoinRequestSql = "DELETE FROM JoinRequest WHERE Id = @Id";
            using (SqlCommand command = new(deleteJoinRequestSql, connection))
            {
                command.Parameters.AddWithValue("@Id", joinRequestId);
                command.ExecuteNonQuery();
            }
        }
    }
}
