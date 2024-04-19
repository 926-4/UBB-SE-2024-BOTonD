using Microsoft.Data.SqlClient;
using Moderation.Entities;
using System.Configuration;

namespace Moderation.DbEndpoints
{
    public class JoinRequestEndpoints
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public static void CreateJoinRequest(JoinRequest joinRequest)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string insertJoinRequestSql = "INSERT INTO JoinRequest (Id, UserId) VALUES (@Id, @UserId)";
            using SqlCommand command = new(insertJoinRequestSql, connection);
            command.Parameters.AddWithValue("@Id", joinRequest.Id);
            command.Parameters.AddWithValue("@UserId", joinRequest.userId);
            command.ExecuteNonQuery();
        }
        public static List<JoinRequest> ReadAllJoinRequests()
        {
            List<JoinRequest> joinRequests = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string sql = "SELECT Junior.Id, Junior.UserId " +
                             "FROM JoinRequest Junior "; 
                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    JoinRequest joinRequest = new(
                        reader.GetGuid(0),
                        reader.GetGuid(1)
                    );
                    joinRequests.Add(joinRequest);
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
