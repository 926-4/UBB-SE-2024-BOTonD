using Moderation.Entities.Report;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Moderation.DbEndpoints
{
    public class ReportEndpoint
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public static void CreatePostReport(PostReport postReport)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "INSERT INTO Report (ReportId, UserId, PostId, Message, GroupId) " +
                         "VALUES (@ReportId, @UserId, @PostId, @Message, @GroupId)";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@ReportId", postReport.Id);
            command.Parameters.AddWithValue("@UserId", postReport.UserId);
            command.Parameters.AddWithValue("@PostId", postReport.PostId);
            command.Parameters.AddWithValue("@Message", postReport.Message);
            command.Parameters.AddWithValue("@GroupId", postReport.GroupId);
            command.ExecuteNonQuery();
        }

        public static List<PostReport> ReadAllPostReports()
        {
            List<PostReport> postReports = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string sql = "SELECT ReportId, UserId, PostId, Message, GroupId FROM Report";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ////!?!?!??!?!?!?!
                    PostReport postReport = new(reader.GetGuid(0), reader.GetGuid(1), reader.GetGuid(2), reader.GetString(3), reader.GetGuid(4));
                    postReports.Add(postReport);
                }
            }

            return postReports;
        }

        public static void DeletePostReport(Guid reportId)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "DELETE FROM Report WHERE ReportId = @ReportId";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@ReportId", reportId);

            command.ExecuteNonQuery();
        }

        public static void UpdatePostReport(Guid Id, PostReport postReport)
        {
            using SqlConnection connection = new(connectionString);

            connection.Open();

            string sqlCommandString = "UPDATE Report" +
                                      $"SET UserId = {postReport.UserId},"  +
                                      $"PostId = {postReport.PostId},"      +
                                      $"Message = {postReport.Message},"    +
                                      $"GroupId = {postReport.GroupId}"     +
                                      $"WHERE ReportId = {Id}";

            using SqlCommand command = new(sqlCommandString, connection);

            command.ExecuteNonQuery();
        }
    }
}