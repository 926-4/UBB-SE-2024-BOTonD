using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Moderation.Entities.Post;
using Moderation.Entities;
namespace Moderation.DbEndpoints
{
    public class PollEndpoints
    {
        private static string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreatePollPost(PollPost pollPost)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertPollPostSql = "INSERT INTO PollPost (PollId, Content, UserId, Score, Status, IsDeleted) " +
                                           "VALUES (@PollId, @Content, @UserId, @Score, @Status, @IsDeleted)";

                using (SqlCommand command = new SqlCommand(insertPollPostSql, connection))
                {
                    command.Parameters.AddWithValue("@PollId", pollPost.postId);
                    command.Parameters.AddWithValue("@Content", pollPost.content);
                    command.Parameters.AddWithValue("@UserId", pollPost.author.Id);
                    command.Parameters.AddWithValue("@Score", pollPost.score);
                    command.Parameters.AddWithValue("@Status", pollPost.status);
                    command.Parameters.AddWithValue("@IsDeleted", pollPost.isDeleted);

                    command.ExecuteNonQuery();
                }

                // Insert options for the poll into PollOption table
                foreach (string option in pollPost.options)
                {
                    string insertPollOptionSql = "INSERT INTO PollOption (PollId, OptionText) VALUES (@PollId, @OptionText)";
                    using (SqlCommand optionCommand = new SqlCommand(insertPollOptionSql, connection))
                    {
                        optionCommand.Parameters.AddWithValue("@PollId", pollPost.postId);
                        optionCommand.Parameters.AddWithValue("@OptionText", option);
                        optionCommand.ExecuteNonQuery();
                    }
                }

                // Insert awards for the poll into PollAward table
                foreach (Award award in pollPost.awards)
                {
                    string insertPollAwardSql = "INSERT INTO PollAward (AwardId, PollId) VALUES (@AwardId, @PollId)";
                    using (SqlCommand awardCommand = new SqlCommand(insertPollAwardSql, connection))
                    {
                        awardCommand.Parameters.AddWithValue("@AwardId", award.awardId);
                        awardCommand.Parameters.AddWithValue("@PollId", pollPost.postId);
                        awardCommand.ExecuteNonQuery();
                    }
                }
            }
        }
        public static List<PollPost> ReadAllPollPosts()
        {
            List<PollPost> pollPosts = new List<PollPost>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT pp.PollId, pp.Content, pp.Score, pp.Status, pp.IsDeleted, " +
                             "u.Id, u.Username, u.PostScore, u.MarketplaceScore, u.StatusRestriction, u.StatusRestrictionDate, u.StatusMessage " +
                             "FROM PollPost pp " +
                             "INNER JOIN GroupUser u ON pp.UserId = u.Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guid pollId = reader.GetGuid(0);
                            string content = reader.GetString(1);
                            int score = reader.GetInt32(2);
                            string status = reader.GetString(3);
                            bool isDeleted = reader.GetBoolean(4);

                            Guid userId = reader.GetGuid(5);
                            string username = reader.GetString(6);
                            int postScore = reader.GetInt32(7);
                            int marketplaceScore = reader.GetInt32(8);
                            int statusRestriction = reader.GetInt32(9);
                            DateTime statusRestrictionDate = reader.GetDateTime(10);
                            string statusMessage = reader.GetString(11);

                            User author = new User(userId, username, postScore, marketplaceScore, new UserStatus((UserRestriction)statusRestriction, statusRestrictionDate, statusMessage));

                            // Fetch options for the poll
                            List<string> options = ReadOptionsForPoll(pollId);

                            // Fetch awards for the poll
                            List<Award> awards = ReadAwardsForPoll(pollId);

                            PollPost pollPost = new PollPost(pollId, content, author, score, status, isDeleted, options, null, awards);
                            pollPosts.Add(pollPost);
                        }
                    }
                }
            }

            return pollPosts;
        }
        private static List<Award> ReadAwardsForPoll(Guid pollId)
        {
            List<Award> awards = new List<Award>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT a.AwardId, a.Type " +
                             "FROM Award a " +
                             "INNER JOIN PollAward pa ON a.AwardId = pa.AwardId " +
                             "WHERE pa.PollId = @PollId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@PollId", pollId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guid awardId = reader.GetGuid(0);
                            string type = reader.GetString(1);

                            Award award = new Award { awardId=awardId,awardType = (Award.AwardType)Enum.Parse(typeof(Award.AwardType), reader.GetString(1)) };
                            awards.Add(award);
                        }
                    }
                }
            }

            return awards;
        }
        // Helper method to read options for a poll
        private static List<string> ReadOptionsForPoll(Guid pollId)
        {
            List<string> options = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT OptionText FROM PollOption WHERE PollId = @PollId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@PollId", pollId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string optionText = reader.GetString(0);
                            options.Add(optionText);
                        }
                    }
                }
            }

            return options;
        }
        public static void DeletePollPost(Guid pollId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Delete from PollAward table first
                string deletePollAwardSql = "DELETE FROM PollAward WHERE PollId = @PollId";
                using (SqlCommand command = new SqlCommand(deletePollAwardSql, connection))
                {
                    command.Parameters.AddWithValue("@PollId", pollId);
                    command.ExecuteNonQuery();
                }

                // Delete from PollOption table next
                string deletePollOptionSql = "DELETE FROM PollOption WHERE PollId = @PollId";
                using (SqlCommand command = new SqlCommand(deletePollOptionSql, connection))
                {
                    command.Parameters.AddWithValue("@PollId", pollId);
                    command.ExecuteNonQuery();
                }

                // Delete from PollPost table
                string deletePollPostSql = "DELETE FROM PollPost WHERE PollId = @PollId";
                using (SqlCommand command = new SqlCommand(deletePollPostSql, connection))
                {
                    command.Parameters.AddWithValue("@PollId", pollId);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
