﻿using System;
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

                string sql = "SELECT p.PostId, p.Content, p.Score, p.Status, p.IsDeleted, p.GroupId" +
                             "u.Id, u.UserId, u.GroupId, u.PostScore, u.MarketplaceScore, u.StatusRestriction, u.StatusRestrictionDate, u.StatusMessage, u.RoleId " +
                             "FROM Post p " +
                             "INNER JOIN GroupUser u ON p.UserId = u.Id";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Guid postid = reader.GetGuid(0);
                    string content = reader.GetString(1);
                    int score = reader.GetInt32(2);
                    string status = reader.GetString(3);
                    bool isdeleted = reader.GetBoolean(4);
                    Guid groupId = reader.GetGuid(5);

                    Guid id = reader.GetGuid(6);
                    Guid userId = reader.GetGuid(7);
                    Guid groupUserId = reader.GetGuid(8);
                    int postscore = reader.GetInt32(9);
                    int marketplacescore = reader.GetInt32(10);
                    int statusrestriction = reader.GetInt32(11);
                    DateTime statusrestrictiondate = reader.GetDateTime(12);
                    string statusmessage = reader.GetString(13);
                    Guid roleId = reader.GetGuid(14);
                    GroupUser author = new(id,userId,groupUserId,postscore,marketplacescore,new UserStatus((UserRestriction)statusrestriction, statusrestrictiondate, statusmessage), roleId);


                   List < Award > awards = ReadAwardsForPost(postid);

                    TextPost textPost = new(postid, content, author, [], score, status, false); 
                    //public TextPost(
                    //Guid id,
                    //string content,
                    //GroupUser author,
                    //List<Award> awards,
                    //int score = 0,
                    //string status = "",
                    //bool isDeleted = false)

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
            string deletePostSql = "DELETE FROM Post WHERE PostId = @Id";
            using (SqlCommand command = new(deletePostSql, connection))
            {
                command.Parameters.AddWithValue("@Id", postId);
                command.ExecuteNonQuery();
            }
        }
    }
}
