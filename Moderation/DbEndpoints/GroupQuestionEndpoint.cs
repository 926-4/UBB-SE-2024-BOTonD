using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.DbEndpoints
{
    public class GroupQuestion
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string TypeofAnswer { get; set; }
    }
    public class GroupQuestionEndpoint
    {
        private static string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateQuestion(GroupQuestion groupQuestion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO JoinRequest VALUES (@Id,@q,@t)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", groupQuestion.Id);
                    command.Parameters.AddWithValue("@q", groupQuestion.Question);
                    command.Parameters.AddWithValue("@t", groupQuestion.TypeofAnswer);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static List<GroupQuestion> ReadQuestion()
        {
            List<GroupQuestion> groupQuestions = new List<GroupQuestion>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM GroupQuestion";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GroupQuestion groupQuestion = new GroupQuestion
                            {
                                Id = reader.GetGuid(0),
                                Question = reader.GetString(1),
                                TypeofAnswer = reader.GetString(2),
                            };
                            groupQuestions.Add(groupQuestion);
                        }
                    }
                }
            }
            return groupQuestions;
        }
        public static void UpdateQuestion(GroupQuestion groupQuestion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE GroupQuestion SET question=@q,typeofAnswer=@t WHERE questionId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@q", groupQuestion.Question);
                    command.Parameters.AddWithValue("@t", groupQuestion.TypeofAnswer);
                    command.Parameters.AddWithValue("@id", groupQuestion.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteQuestion(GroupQuestion groupQuestion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM GroupQuestion WHERE questionId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", groupQuestion.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
        
    }
}
