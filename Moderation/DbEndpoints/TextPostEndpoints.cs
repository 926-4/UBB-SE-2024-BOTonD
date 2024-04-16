using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Moderation.Entities.Post;
using Moderation.Entities;

namespace Moderation.DbEndpoints
{
    public class TextPostEndpoints
    {
        private static string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateTextPost(TextPost textPost)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertPostSql = "INSERT INTO Post (PostId, Content, UserId, Score, Status, IsDeleted) " +
                                       "VALUES (@PostId, @Content, @UserId, @Score, @Status, @IsDeleted)";

                using (SqlCommand command = new SqlCommand(insertPostSql, connection))
                {
                    command.Parameters.AddWithValue("@PostId", textPost.postId);
                    command.Parameters.AddWithValue("@Content", textPost.content);
                    command.Parameters.AddWithValue("@UserId", textPost.author.Id);
                    command.Parameters.AddWithValue("@Score", textPost.score);
                    command.Parameters.AddWithValue("@Status", textPost.status);
                    command.Parameters.AddWithValue("@IsDeleted", textPost.isDeleted);

                    command.ExecuteNonQuery();
                }

                // Insert awards for the post into PostAward table
                foreach (Award award in textPost.awards)
                {
                    string insertPostAwardSql = "INSERT INTO PostAward (AwardId, PostId) VALUES (@AwardId, @PostId)";
                    using (SqlCommand awardCommand = new SqlCommand(insertPostAwardSql, connection))
                    {
                        awardCommand.Parameters.AddWithValue("@AwardId", award.awardId);
                        awardCommand.Parameters.AddWithValue("@PostId", textPost.postId);
                        awardCommand.ExecuteNonQuery();
                    }
                }
            }
        }
        public static List<TextPost> ReadAllTextPosts()
        {
            List<TextPost> textPosts = new List<TextPost>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT p.PostId, p.Content, p.Score, p.Status, p.IsDeleted, " +
                             "u.Id, u.Username, u.PostScore, u.MarketplaceScore, u.StatusRestriction, u.StatusRestrictionDate, u.StatusMessage " +
                             "FROM Post p " +
                             "INNER JOIN GroupUser u ON p.UserId = u.Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guid postId = reader.GetGuid(0);
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

                            // Fetch awards for the post
                            List<Award> awards = ReadAwardsForPost(postId);

                            TextPost textPost = new TextPost(postId, content, author, score, status, awards, isDeleted);
                            textPosts.Add(textPost);
                        }
                    }
                }
            }

            return textPosts;
        }
        private static List<Award> ReadAwardsForPost(Guid postId)
        {
            List<Award> awards = new List<Award>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT a.AwardId, a.Type " +
                             "FROM Award a " +
                             "INNER JOIN PostAward pa ON a.AwardId = pa.AwardId " +
                             "WHERE pa.PostId = @PostId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@PostId", postId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guid awardId = reader.GetGuid(0);
                            Award award = new Award { awardId=awardId,awardType= (Award.AwardType)Enum.Parse(typeof(Award.AwardType), reader.GetString(1)) };
                            awards.Add(award);
                        }
                    }
                }
            }

            return awards;
        }
        public static void DeleteTextPost(Guid postId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Delete from PostAward table first
                string deletePostAwardSql = "DELETE FROM PostAward WHERE PostId = @PostId";
                using (SqlCommand command = new SqlCommand(deletePostAwardSql, connection))
                {
                    command.Parameters.AddWithValue("@PostId", postId);
                    command.ExecuteNonQuery();
                }

                // Delete from Post table
                string deletePostSql = "DELETE FROM Post WHERE PostId = @PostId";
                using (SqlCommand command = new SqlCommand(deletePostSql, connection))
                {
                    command.Parameters.AddWithValue("@PostId", postId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
