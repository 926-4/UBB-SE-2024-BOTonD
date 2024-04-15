using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;

namespace Moderation.DbEndpoints
{
    public class Report
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }

    public class ReportEndpoint
    {
        private static string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateReport(Report report)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Report VALUES (@Id,@u,@m,@s,@t)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", report.Id);
                    command.Parameters.AddWithValue("@u", report.UserId);
                    command.Parameters.AddWithValue("@m", report.Message);
                    command.Parameters.AddWithValue("@s", report.Status);
                    command.Parameters.AddWithValue("@t", report.Type);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static List<Report> ReadReport()
        {
            List<Report> reports = new List<Report>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Report";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Report report = new Report
                            {
                                Id = reader.GetGuid(0),
                                UserId = reader.GetGuid(1),
                                Message = reader.GetString(2),
                                Status = reader.GetString(3),
                                Type = reader.GetString(4)
                            };
                            reports.Add(report);
                        }
                    }
                }
            }
            return reports;
        }
        public static void UpdateReport(Report report)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE Report SET userId=@u, message=@m, status=@s, type=@t WHERE reportId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@u", report.UserId);
                    command.Parameters.AddWithValue("@m", report.Message);
                    command.Parameters.AddWithValue("@s", report.Status);
                    command.Parameters.AddWithValue("@t", report.Type);
                    command.Parameters.AddWithValue("@id", report.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteReport(Report report)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Repost WHERE reportId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", report.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
