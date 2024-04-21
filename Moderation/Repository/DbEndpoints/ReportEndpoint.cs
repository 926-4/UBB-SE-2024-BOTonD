using Microsoft.Data.SqlClient;
using Moderation.Entities.Report;
using Moderation.Serivce;
using System.Configuration;

namespace Moderation.DbEndpoints
{
    public class ReportEndpoint
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private static readonly Dictionary<Guid, PostReport> hardcodedReports = new()
        {
            {Guid.Parse("AA0B1530-2AAF-489B-AFAB-56EA4F95980B"),
                new PostReport(Guid.Parse("AA0B1530-2AAF-489B-AFAB-56EA4F95980B"),
                    Guid.Parse("B05ABC1A-8952-41FB-A503-BFAD23CA9092"), // The Reporter
                    Guid.Parse("EC492AE1-D795-442E-9F64-88DC19CA8F6E"), // The post
                    "This is not a nice post" ,
                    Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973")) // The Group
            },
            {
                Guid.Parse("E59FACA6-8FFB-450B-8017-FF9F111E8A95"),
                new PostReport(Guid.Parse("E59FACA6-8FFB-450B-8017-FF9F111E8A95"),
                    Guid.Parse("B05ABC1A-8952-41FB-A503-BFAD23CA9092"), // The Reporter
                    Guid.Parse("97BE5A68-F673-4AF5-BDE5-0D7D7D7DE27A"), // The post
                    "This is even worse!" ,
                    Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"))  // The Group
            }
        };
        public static void CreatePostReport(PostReport postReport)
        {

            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                hardcodedReports.Add(postReport.Id, postReport);
                return;
            }
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                ApplicationState.Get().DbConnectionIsAvailable = false;
                hardcodedReports.Add(postReport.Id, postReport);
                return;
            }

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
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                return [.. hardcodedReports.Values];
            }
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                ApplicationState.Get().DbConnectionIsAvailable = false;
                return [.. hardcodedReports.Values];
            }
            List<PostReport> postReports = [];


            string sql = "SELECT ReportId, UserId, PostId, Message, GroupId FROM Report";

            using SqlCommand command = new(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                PostReport postReport = new(reader.GetGuid(0), reader.GetGuid(1), reader.GetGuid(2), reader.GetString(3), reader.GetGuid(4));
                postReports.Add(postReport);
            }


            return postReports;
        }

        public static void DeletePostReport(Guid reportId)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                hardcodedReports.Remove(reportId);
                return;
            }
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                ApplicationState.Get().DbConnectionIsAvailable = false;
                hardcodedReports.Remove(reportId);
                return;
            }

            string sql = "DELETE FROM Report WHERE ReportId = @ReportId";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@ReportId", reportId);

            command.ExecuteNonQuery();
        }

        public static void UpdatePostReport(Guid Id, PostReport postReport)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                if (!hardcodedReports.ContainsKey(Id))
                    return;
                hardcodedReports[Id] = postReport;
                return;
            }
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                ApplicationState.Get().DbConnectionIsAvailable = false;
                if (!hardcodedReports.ContainsKey(Id))
                    return;
                hardcodedReports[Id] = postReport;
                return;
            }
            string sqlCommandString = "UPDATE Report" +
                                      $"SET UserId = {postReport.UserId}," +
                                      $"PostId = {postReport.PostId}," +
                                      $"Message = {postReport.Message}," +
                                      $"GroupId = {postReport.GroupId}" +
                                      $"WHERE ReportId = {Id}";

            using SqlCommand command = new(sqlCommandString, connection);

            command.ExecuteNonQuery();
        }
    }
}