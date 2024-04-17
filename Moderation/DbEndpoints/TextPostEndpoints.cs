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
        private static readonly string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateTextPost(TextPost textPost)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string insertPostSql = "INSERT INTO Post (Id, Content, UserId, Score, Status, IsDeleted) " +
                                   "VALUES (@Id, @Content, @UserId, @Score, @Status, @IsDeleted)";

            using (SqlCommand command = new(insertPostSql, connection))
            {
                command.Parameters.AddWithValue("@Id", textPost.Id);
                command.Parameters.AddWithValue("@Content", textPost.Content);
                command.Parameters.AddWithValue("@UserId", textPost.Author.Id);
                command.Parameters.AddWithValue("@Score", textPost.Score);
                command.Parameters.AddWithValue("@Status", textPost.Status);
                command.Parameters.AddWithValue("@IsDeleted", textPost.IsDeleted);

                command.ExecuteNonQuery();
            }

            // Insert awards for the post into PostAward table
            foreach (Award award in textPost.Awards)
            {
                string insertPostAwardSql = "INSERT INTO PostAward (AwardId, Id) VALUES (@AwardId, @Id)";
                using SqlCommand awardCommand = new(insertPostAwardSql, connection);
                awardCommand.Parameters.AddWithValue("@AwardId", award.awardId);
                awardCommand.Parameters.AddWithValue("@Id", textPost.Id);
                awardCommand.ExecuteNonQuery();
            }
        }
        public static List<TextPost> ReadAllTextPosts()
        {
            List<TextPost> textPosts = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string sql = "SELECT p.Id, p.Content, p.Score, p.Status, p.IsDeleted, " +
                             "u.Id, u.Username, u.PostScore, u.MarketplaceScore, u.StatusRestriction, u.StatusRestrictionDate, u.StatusMessage " +
                             "FROM Post p " +
                             "INNER JOIN GroupUser u ON p.UserId = u.Id";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //Guid postId = reader.GetGuid(0);
                    string content = reader.GetString(1);
                    //int score = reader.GetInt32(2);
                    //string status = reader.GetString(3);
                    //bool isDeleted = reader.GetBoolean(4);

                    //Guid userId = reader.GetGuid(5);
                    string username = reader.GetString(6);
                    //int postScore = reader.GetInt32(7);
                    //int marketplaceScore = reader.GetInt32(8);
                    //int statusRestriction = reader.GetInt32(9);
                    //DateTime statusRestrictionDate = reader.GetDateTime(10);
                    //string statusMessage = reader.GetString(11);

                    User author = new(username);

                    // Fetch awards for the post
                    //List<Award> awards = ReadAwardsForPost(postId);

                    TextPost textPost = new(content, author);
                    textPosts.Add(textPost);
                }
            }

            return textPosts;
        }
        private static List<Award> ReadAwardsForPost(Guid postId)
        {
            List<Award> awards = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string sql = "SELECT a.AwardId, a.Type " +
                             "FROM Award a " +
                             "INNER JOIN PostAward pa ON a.AwardId = pa.AwardId " +
                             "WHERE pa.Id = @Id";

                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@Id", postId);

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Guid awardId = reader.GetGuid(0);
                    Award award = new() { awardId = awardId, awardType = (Award.AwardType)Enum.Parse(typeof(Award.AwardType), reader.GetString(1)) };
                    awards.Add(award);
                }
            }

            return awards;
        }
        public static void DeleteTextPost(Guid postId)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            // Delete from PostAward table first
            string deletePostAwardSql = "DELETE FROM PostAward WHERE Id = @Id";
            using (SqlCommand command = new(deletePostAwardSql, connection))
            {
                command.Parameters.AddWithValue("@Id", postId);
                command.ExecuteNonQuery();
            }

            // Delete from Post table
            string deletePostSql = "DELETE FROM Post WHERE Id = @Id";
            using (SqlCommand command = new(deletePostSql, connection))
            {
                command.Parameters.AddWithValue("@Id", postId);
                command.ExecuteNonQuery();
            }
        }
    }
}
