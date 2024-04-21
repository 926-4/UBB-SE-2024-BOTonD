using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Entities.Post;
using Moderation.Serivce;
using System.Configuration;

namespace Moderation.DbEndpoints
{
    public class TextPostEndpoints
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private static readonly Dictionary<Guid, TextPost> hardcodedPosts = new()
        {
            {Guid.Parse("2077F417-CB31-4728-B5BB-3AA57239BBCD"),
                new TextPost(Guid.Parse("2077F417-CB31-4728-B5BB-3AA57239BBCD"), "Hello! Welcome!", Guid.Parse("B05ABC1A-8952-41FB-A503-BFAD23CA9092")) },
            {Guid.Parse("EC492AE1-D795-442E-9F64-88DC19CA8F6E"),
                new TextPost(Guid.Parse("EC492AE1-D795-442E-9F64-88DC19CA8F6E"), "This post is very problematic >:-(", Guid.Parse("4CCA015B-D068-43B1-8839-08D767391769")) },
            {Guid.Parse("97BE5A68-F673-4AF5-BDE5-0D7D7D7DE27A"),
                new TextPost(Guid.Parse("97BE5A68-F673-4AF5-BDE5-0D7D7D7DE27A"), "I hate some people in this group", Guid.Parse("4CCA015B-D068-43B1-8839-08D767391769")) },
            {Guid.Parse("6AF9EF40-EE0B-4123-BA8A-D38B193C77B6"),
                new TextPost(Guid.Parse("6AF9EF40-EE0B-4123-BA8A-D38B193C77B6"), "Happy birthday!!", Guid.Parse("18282CBC-4225-498D-AB48-8E8B31466759")) },
            {Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C662B"),
                new TextPost(Guid.Parse("AC60415D-2442-491D-BCA8-CBAB6A1C662B"), "Thanks everyone!", Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692")) }
        };
        public static void CreateTextPost(TextPost textPost)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                hardcodedPosts.Add(textPost.Id, textPost);
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
                hardcodedPosts.Add(textPost.Id, textPost);
                return;
            }

            string insertPostSql = "INSERT INTO Post (PostId, Content, UserId, Score, Status, IsDeleted, GroupId) " +
                                   "VALUES (@Id, @Content, @UserId, @Score, @Status, @IsDeleted, @GroupId)";

            using (SqlCommand command = new(insertPostSql, connection))
            {
                command.Parameters.AddWithValue("@Id", textPost.Id);
                command.Parameters.AddWithValue("@Content", textPost.Content);
                command.Parameters.AddWithValue("@UserId", textPost.Author.Id);
                command.Parameters.AddWithValue("@Score", textPost.Score);
                command.Parameters.AddWithValue("@Status", textPost.Status);
                command.Parameters.AddWithValue("@IsDeleted", textPost.IsDeleted);
                command.Parameters.AddWithValue("@GroupId", textPost.Author.GroupId);

                command.ExecuteNonQuery();
            }

            // Insert hardcodedAwards for the post into PostAward table
            foreach (Award award in textPost.Awards)
            {
                string insertPostAwardSql = "INSERT INTO PostAward (AwardId, Id) VALUES (@AwardId, @Id)";
                using SqlCommand awardCommand = new(insertPostAwardSql, connection);
                awardCommand.Parameters.AddWithValue("@AwardId", award.Id);
                awardCommand.Parameters.AddWithValue("@Id", textPost.Id);
                awardCommand.ExecuteNonQuery();
            }
        }
        public static List<TextPost> ReadAllTextPosts()
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                return [.. hardcodedPosts.Values];
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
                return [.. hardcodedPosts.Values];
            }
            List<TextPost> textPosts = [];

            string sql = "SELECT p.PostId, p.Content, p.GroupId, " +
                         "u.Id, u.Uid , u.GroupId " +
                         "FROM Post p " +
                         "INNER JOIN GroupUser u ON p.UserId = u.Id";

            using SqlCommand command = new(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Guid postid = reader.GetGuid(0);
                string content = reader.GetString(1);
                //Guid groupId = reader.GetGuid(2);

                Guid id = reader.GetGuid(3);
                Guid userId = reader.GetGuid(4);
                Guid groupUserId = reader.GetGuid(5);

                GroupUser author = new(id, userId, groupUserId);


                List<Award> awards = ReadAwardsForPost(postid);

                TextPost textPost = new(postid, content, author, awards);


                textPosts.Add(textPost);
            }

            return textPosts;
        }
        private static List<Award> ReadAwardsForPost(Guid postId)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                return [];
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
                return [];
            }
            List<Award> awards = [];

            string sql = "SELECT a.AwardId, a.Type " +
                         "FROM Award a " +
                         "INNER JOIN PostAward pa ON a.AwardId = pa.AwardId " +
                         "WHERE pa.PostId = @Id";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", postId);

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Guid awardId = reader.GetGuid(0);
                Award award = new() { Id = awardId, awardType = (Award.AwardType)Enum.Parse(typeof(Award.AwardType), reader.GetString(1)) };
                awards.Add(award);
            }

            return awards;
        }
        public static void DeleteTextPost(Guid postId)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                hardcodedPosts.Remove(postId);
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
                hardcodedPosts.Remove(postId);
                return;
            }

            // Delete from PostAward table first
            string deletePostAwardSql = "DELETE FROM PostAward WHERE Id = @Id";
            using (SqlCommand command = new(deletePostAwardSql, connection))
            {
                command.Parameters.AddWithValue("@Id", postId);
                command.ExecuteNonQuery();
            }

            // Delete from Post table
            string deletePostSql = "DELETE FROM Post WHERE PostId = @Id";
            using (SqlCommand command = new(deletePostSql, connection))
            {
                command.Parameters.AddWithValue("@Id", postId);
                command.ExecuteNonQuery();
            }
        }
        public static void UpdateTextPost(TextPost textPost)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                if (!hardcodedPosts.ContainsKey(textPost.Id))
                    return;
                hardcodedPosts[textPost.Id] = textPost;
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
                if (!hardcodedPosts.ContainsKey(textPost.Id))
                    return;
                hardcodedPosts[textPost.Id] = textPost;
                return;
            }
            string updatePostSql = "UPDATE Post SET " +
                                   "Content = @Content, " +
                                   "UserId = @UserId, " +
                                   "Score = @Score, " +
                                   "Status = @Status, " +
                                   "IsDeleted = @IsDeleted, " +
                                   "GroupId = @GroupId " +
                                   "WHERE PostId = @Id";

            using SqlCommand command = new(updatePostSql, connection);
            command.Parameters.AddWithValue("@Id", textPost.Id);
            command.Parameters.AddWithValue("@Content", textPost.Content);
            command.Parameters.AddWithValue("@UserId", textPost.Author.Id);
            command.Parameters.AddWithValue("@Score", textPost.Score);
            command.Parameters.AddWithValue("@Status", textPost.Status);
            command.Parameters.AddWithValue("@IsDeleted", textPost.IsDeleted);
            command.Parameters.AddWithValue("@GroupId", textPost.Author.GroupId);

            command.ExecuteNonQuery();
        }
    }
}
