using Moderation.Entities;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Moderation.DbEndpoints
{
    public class GroupUserEndpoints
    {
        private static readonly string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    
        public static void CreateGroupUser(GroupUser user)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "INSERT INTO GroupUser (Id, Uid, GroupId, PostScore, MarketplaceScore, StatusRestriction, StatusRestrictionDate, StatusMessage, RoleId) " +
                         "VALUES (@Id, @Uid, @GroupId, @PostScore, @MarketplaceScore, @StatusRestriction, @StatusRestrictionDate, @StatusMessage, @RoleId)";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Uid", user.UserId);
            command.Parameters.AddWithValue("@GroupId", user.GroupId);
            command.Parameters.AddWithValue("@PostScore", user.PostScore);
            command.Parameters.AddWithValue("@MarketplaceScore", user.MarketplaceScore);
            command.Parameters.AddWithValue("@StatusRestriction", (int)user.Status.Restriction);
            command.Parameters.AddWithValue("@StatusRestrictionDate", user.Status.RestrictionDate);
            command.Parameters.AddWithValue("@StatusMessage", user.Status.Message);
            command.Parameters.AddWithValue("@RoleId", user.RoleId);

            command.ExecuteNonQuery();
        }
        public static List<GroupUser> ReadAllGroupUsers()
        {
            List<GroupUser> users = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string sql = "SELECT Id, Uid, Groupid, PostScore, MarketplaceScore, StatusRestriction, StatusRestrictionDate, StatusMessage, RoleId FROM GroupUser";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserRestriction restriction = (UserRestriction)reader.GetInt32(5);
                    UserStatus status = new(restriction, reader.GetDateTime(6), reader.GetString(7));

                    GroupUser user = new GroupUser(reader.GetGuid(0), reader.GetGuid(1), reader.GetGuid(2), reader.GetInt32(3), reader.GetInt32(4), status, reader.GetGuid(8));
                    users.Add(user);
                }
            }

            return users;
        }
        public static void UpdateGroupUser(GroupUser user)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "UPDATE GroupUser SET" +
                         "Uid = @Uid, GroupId = @GroupId," +
                         "PostScore = @PostScore, MarketplaceScore = @MarketplaceScore, " +
                         "StatusRestriction = @StatusRestriction, StatusRestrictionDate = @StatusRestrictionDate, StatusMessage = @StatusMessage " +
                         "RoleId = @RoleId" +
                         "WHERE Id = @Id";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Uid", user.UserId);
            command.Parameters.AddWithValue("@GroupId", user.GroupId);
            command.Parameters.AddWithValue("@PostScore", user.PostScore);
            command.Parameters.AddWithValue("@MarketplaceScore", user.MarketplaceScore);
            command.Parameters.AddWithValue("@StatusRestriction", (int)user.Status.Restriction);
            command.Parameters.AddWithValue("@StatusRestrictionDate", user.Status.RestrictionDate);
            command.Parameters.AddWithValue("@StatusMessage", user.Status.Message);
            command.Parameters.AddWithValue("@RoleId", user.RoleId);

            command.ExecuteNonQuery();
        }
        public static void DeleteGroupUser(Guid id)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "DELETE FROM GroupUser WHERE Id = @Id";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }
}