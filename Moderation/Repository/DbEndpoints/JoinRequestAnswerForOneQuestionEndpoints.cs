using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Serivce;
using System.Configuration;

namespace Moderation.DbEndpoints
{
    public class JoinRequestAnswerForOneQuestionEndpoints
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private static readonly Dictionary<Guid, JoinRequestAnswerToOneQuestion> hardcodedAnswers = new()
        {
            {
                Guid.Parse("A6006EE8-5D2C-4BB1-9761-334F59982987"),
                new JoinRequestAnswerToOneQuestion(
                    Guid.Parse("A6006EE8-5D2C-4BB1-9761-334F59982987"),
                    Guid.Parse("4E965DCE-66AC-4040-9E65-BE0BEE465928"),
                    "How are you?",
                    "Good. you?")
            },{
                Guid.Parse("13F979AC-F705-439C-AEBE-219DC37456FC"),
                new JoinRequestAnswerToOneQuestion(
                    Guid.Parse("13F979AC-F705-439C-AEBE-219DC37456FC"),
                    Guid.Parse("4E965DCE-66AC-4040-9E65-BE0BEE465928"),
                    "When are you free?",
                    "May 1st 2024")
            },{
                Guid.Parse("26D6137F-147C-4005-8DDE-16A26511540E"),
                new JoinRequestAnswerToOneQuestion(
                    Guid.Parse("26D6137F-147C-4005-8DDE-16A26511540E"),
                    Guid.Parse("4E965DCE-66AC-4040-9E65-BE0BEE465928"),
                    "Favourite farm animal",
                    "Sheep")
            }
        };
        public static void CreateQuestion(JoinRequestAnswerToOneQuestion question)
        {

            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                hardcodedAnswers.Add(question.Id, question);
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
                hardcodedAnswers.Add(question.Id, question);
                return;
            }
            string sql = "INSERT INTO JoinRequestMessage VALUES (@JoinRequestId,@[Key], @[Value])";
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@JoinRequest", question.RequestId);
            command.Parameters.AddWithValue("@[Key]", question.QuestionText);
            command.Parameters.AddWithValue("@[Value]", question.QuestionAnswer);
            command.ExecuteNonQuery();
        }
        public static List<JoinRequestAnswerToOneQuestion> ReadQuestion()
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                return [.. hardcodedAnswers.Values];
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
                return [.. hardcodedAnswers.Values];
            }
            List<JoinRequestAnswerToOneQuestion> allAnswersToAllQuestions = [];

            string sql = "SELECT * FROM JoinRequestMessage";
            using SqlCommand command = new(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                JoinRequestAnswerToOneQuestion qAndA = new(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
                allAnswersToAllQuestions.Add(qAndA);
            }

            return allAnswersToAllQuestions;
        }
        public static void UpdateQuestion(JoinRequestAnswerToOneQuestion question)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                if (!hardcodedAnswers.ContainsKey(question.Id))
                    return;
                hardcodedAnswers[question.Id] = question;
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
                if (!hardcodedAnswers.ContainsKey(question.Id))
                    return;
                hardcodedAnswers[question.Id] = question;
                return;
            }
            string sql = "UPDATE JoinRequestMessage SET [Value]=@[Value] WHERE JoinRequestId=@JoinRequestId AND [Key]=@[Key]";
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@JoinRequest", question.RequestId);
            command.Parameters.AddWithValue("@[Key]", question.QuestionText);
            command.Parameters.AddWithValue("@[Value]", question.QuestionAnswer);
            command.ExecuteNonQuery();
        }
        public static void DeleteQuestion(JoinRequestAnswerToOneQuestion question)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                hardcodedAnswers.Remove(question.Id);
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
                hardcodedAnswers.Remove(question.Id); ;
                return;
            }
            string sql = "DELETE FROM JoinRequestMessage WHERE JoinRequestId=@JoinRequestId AND [Key]=@[Key]";
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@JoinRequest", question.RequestId);
            command.Parameters.AddWithValue("@[Key]", question.QuestionText);
            command.ExecuteNonQuery();
        }
    }
}
