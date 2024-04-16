using Moderation.Entities;
using System.Data.SqlClient;

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

            // Insert JoinRequestMessage entries
            string insertJoinRequestMessageSql = "INSERT INTO JoinRequestMessage (JoinRequestId, [Key], [Value]) VALUES (@JoinRequestId, @Key, @Value)";
            foreach (var kvp in joinRequest.messageResponse)
            {
                using SqlCommand command = new(insertJoinRequestMessageSql, connection);
                command.Parameters.AddWithValue("@JoinRequestId", joinRequest.Id);
                command.Parameters.AddWithValue("@Key", kvp.Key);
                command.Parameters.AddWithValue("@Value", kvp.Value);
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
                             "FROM JoinRequest j " +
                             "JOIN JoinRequestMessage m ON j.Id = m.JoinRequestId";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                Guid currentJoinRequestId = Guid.Empty;
                Dictionary<string, string> messageResponse = [];

                while (reader.Read())
                {
                    Guid joinRequestId = reader.GetGuid(0);

                    if (joinRequestId != currentJoinRequestId)
                    {
                        if (currentJoinRequestId != Guid.Empty)
                        {
                            JoinRequest joinRequest = new(currentJoinRequestId, messageResponse);
                            joinRequests.Add(joinRequest);
                            messageResponse = [];
                        }

                        currentJoinRequestId = joinRequestId;
                    }

                    messageResponse.Add(reader.GetString(2), reader.GetString(3));
                }

                // Add the last join request
                if (currentJoinRequestId != Guid.Empty)
                {
                    JoinRequest joinRequest = new(currentJoinRequestId, messageResponse);
                    joinRequests.Add(joinRequest);
                }
            }

            return joinRequests;
        }

        public static void DeleteJoinRequest(Guid joinRequestId)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string deleteJoinRequestMessageSql = "DELETE FROM JoinRequestMessage WHERE JoinRequestId = @JoinRequestId";
            using (SqlCommand command = new(deleteJoinRequestMessageSql, connection))
            {
                command.Parameters.AddWithValue("@JoinRequestId", joinRequestId);
                command.ExecuteNonQuery();
            }

            string deleteJoinRequestSql = "DELETE FROM JoinRequest WHERE Id = @Id";
            using (SqlCommand command = new(deleteJoinRequestSql, connection))
            {
                command.Parameters.AddWithValue("@Id", joinRequestId);
                command.ExecuteNonQuery();
            }
        }
    }
}
