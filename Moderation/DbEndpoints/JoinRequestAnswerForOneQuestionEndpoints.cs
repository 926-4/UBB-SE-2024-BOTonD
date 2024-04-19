using Microsoft.Data.SqlClient;
using Moderation.Entities;
using System.Configuration;

namespace Moderation.DbEndpoints
{
    public class JoinRequestAnswerForOneQuestionEndpoints
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public static void CreateQuestion(JoinRequestAnswerToOneQuestion question)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "INSERT INTO JoinRequestMessage VALUES (@JoinRequestId,@[Key], @[Value])";
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@JoinRequest", question.RequestId);
            command.Parameters.AddWithValue("@[Key]", question.QuestionText);
            command.Parameters.AddWithValue("@[Value]", question.QuestionAnswer);
            command.ExecuteNonQuery();
        }
        public static List<JoinRequestAnswerToOneQuestion> ReadQuestion()
        {
            List<JoinRequestAnswerToOneQuestion> allAnswersToAllQuestions = [];
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM JoinRequestMessage";
                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    JoinRequestAnswerToOneQuestion qAndA = new(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
                    allAnswersToAllQuestions.Add(qAndA);
                }
            }
            return allAnswersToAllQuestions;
        }
        public static void UpdateQuestion(JoinRequestAnswerToOneQuestion question)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "UPDATE JoinRequestMessage SET [Value]=@[Value] WHERE JoinRequestId=@JoinRequestId AND [Key]=@[Key]";
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@JoinRequest", question.RequestId);
            command.Parameters.AddWithValue("@[Key]", question.QuestionText);
            command.Parameters.AddWithValue("@[Value]", question.QuestionAnswer);
            command.ExecuteNonQuery();
        }
        public static void DeleteQuestion(JoinRequestAnswerToOneQuestion question)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "DELETE FROM JoinRequestMessage WHERE JoinRequestId=@JoinRequestId AND [Key]=@[Key]";
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@JoinRequest", question.RequestId);
            command.Parameters.AddWithValue("@[Key]", question.QuestionText);
            command.ExecuteNonQuery();
        }
    }
}
