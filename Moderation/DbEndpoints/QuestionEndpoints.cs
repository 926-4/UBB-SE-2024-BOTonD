using Moderation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Moderation.DbEndpoints
{
    public class QuestionEndpoints
    {
        private static string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateQuestion(Question question)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO JoinRequestMessage VALUES (@JoinRequestId,@[Key], @[Value])";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@JoinRequest", question.RequestId);
                    command.Parameters.AddWithValue("@[Key]", question.Text);
                    command.Parameters.AddWithValue("@[Value]", question.Answer);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static List<Question> ReadQuestion()
        {
            List<Question> questions = new List<Question>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM JoinRequestMessage";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Question question = new Question(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
                            questions.Add(question);
                        }
                    }
                }
            }
            return questions;
        }
        public static void UpdateQuestion(Question question)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE JoinRequestMessage SET [Value]=@[Value] WHERE JoinRequestId=@JoinRequestId AND [Key]=@[Key], ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@JoinRequest", question.RequestId);
                    command.Parameters.AddWithValue("@[Key]", question.Text);
                    command.Parameters.AddWithValue("@[Value]", question.Answer);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteQuestion(Question question)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM JoinRequestMessage WHERE JoinRequestId=@JoinRequestId AND [Key]=@[Key]";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@JoinRequest", question.RequestId);
                    command.Parameters.AddWithValue("@[Key]", question.Text);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
