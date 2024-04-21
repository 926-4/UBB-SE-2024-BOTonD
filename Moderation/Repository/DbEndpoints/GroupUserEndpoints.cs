using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Model;
using Moderation.Serivce;
using System.Configuration;
namespace Moderation.DbEndpoints
{
    public class GroupUserEndpoints
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        private static readonly Dictionary<Guid, GroupUser> hardcodedGroupUsers = new()
        {
            { Guid.Parse("B05ABC1A-8952-41FB-A503-BFAD23CA9092"),
                new GroupUser(
                    Guid.Parse("B05ABC1A-8952-41FB-A503-BFAD23CA9092"),
                /*User*/Guid.Parse("B7CCB450-EE32-4BFF-8383-E0A0F36CAC06"),   //victor 
                /*Group*/Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"), //victor's study group
                /*Post score*/          1,
                /*Marketplace Score*/   1,
                new UserStatus(UserRestriction.None, DateTime.Now),
                /*Role*/Guid.Parse("00E25F4D-6C60-456B-92CF-D37751176177")) // creator
            },
            { Guid.Parse("4CCA015B-D068-43B1-8839-08D767391769"),
                new GroupUser(
                    Guid.Parse("4CCA015B-D068-43B1-8839-08D767391769"),
                /*User*/Guid.Parse("0825D1FD-C40B-4926-A128-2D924D564B3E"),  //boti
                /*Group*/Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"),//victor's study group
                /*Post score*/          1,
                /*Marketplace Score*/   1,
                new UserStatus(UserRestriction.None, DateTime.Now),
                /*Role*/Guid.Parse("5B4432BD-7A3C-463C-8A4B-34E4BF452AC3"))//member
            },{ Guid.Parse("4017CB13-22B0-43B7-A111-50154C62CC6C"),
                new GroupUser(
                    Guid.Parse("4017CB13-22B0-43B7-A111-50154C62CC6C"),
                /*User*/Guid.Parse("E17FF7A1-95DF-4EAE-8A69-9B139CCD7CA8"),  //norby
                /*Group*/Guid.Parse("3E0F1ED0-8EAF-4D71-AFC7-07D62FFEF973"),//victor's study group
                /*Post score*/          1,
                /*Marketplace Score*/   1,
                new UserStatus(UserRestriction.None, DateTime.Now),
                /*Role*/Guid.Parse("5DEEE3BF-C6A2-4FD2-8E8E-BCA475F4BD44"))//pending
            },
            { Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692"),
                new GroupUser(
                    Guid.Parse("3E7EF48E-2C84-4104-A9B1-3FC60209F692"),
                /*User*/Guid.Parse("9EBE3762-1CD6-45BD-AF9F-0D221CB078D1"),  //izabella
                /*Group*/Guid.Parse("BC5F8CED-50D2-4EF3-B3FD-18217D3F9F3A"),//izabella's bd party
                /*Post score*/          1,
                /*Marketplace Score*/   1,
                new UserStatus(UserRestriction.None, DateTime.Now),
                /*Role*/Guid.Parse("00E25F4D-6C60-456B-92CF-D37751176177")) //creator
            },
            { Guid.Parse("18282CBC-4225-498D-AB48-8E8B31466759"),
                new GroupUser(
                    Guid.Parse("18282CBC-4225-498D-AB48-8E8B31466759"),
                /*User*/Guid.Parse("B7CCB450-EE32-4BFF-8383-E0A0F36CAC06"),  //victor
                /*Group*/Guid.Parse("BC5F8CED-50D2-4EF3-B3FD-18217D3F9F3A"),//izabella's bd party
                /*Post score*/          1,
                /*Marketplace Score*/   1,
                new UserStatus(UserRestriction.None, DateTime.Now),
                /*Role*/Guid.Parse("5B4432BD-7A3C-463C-8A4B-34E4BF452AC3"))//member
            }

        };

        public static void CreateGroupUser(GroupUser user)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                hardcodedGroupUsers.Add(user.Id, user);
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
                hardcodedGroupUsers.Add(user.Id, user);
                return;
            }
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
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                return [.. hardcodedGroupUsers.Values];
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
                return [.. hardcodedGroupUsers.Values];
            }
            List<GroupUser> users = [];


            string sql = "SELECT Id, Uid, Groupid, PostScore, MarketplaceScore, StatusRestriction, StatusRestrictionDate, StatusMessage, RoleId FROM GroupUser";

            using SqlCommand command = new(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //UserRestriction restriction = (UserRestriction)reader.GetInt32(5);
                //UserStatus status = new(restriction, reader.GetDateTime(6), reader.GetString(7));

                GroupUser user = new(reader.GetGuid(0), reader.GetGuid(1), reader.GetGuid(2), reader.GetInt32(3), reader.GetInt32(4), new UserStatus(UserRestriction.None, DateTime.Now), reader.GetGuid(8));
                users.Add(user);

            }

            return users;
        }
        public static void UpdateGroupUser(GroupUser user)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                if (!hardcodedGroupUsers.ContainsKey(user.Id))
                    return;
                hardcodedGroupUsers[user.Id] = user;
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
                if (!hardcodedGroupUsers.ContainsKey(user.Id))
                    return;
                hardcodedGroupUsers[user.Id] = user;
                return;
            }

            string sql = "UPDATE GroupUser SET" +
                         "Uid = @Uid, " +
                         "GroupId = @GroupId, " +
                         "PostScore = @PostScore, " +
                         "MarketplaceScore = @MarketplaceScore, " +
                         "StatusRestriction = @StatusRestriction, " +
                         "StatusRestrictionDate = @StatusRestrictionDate, " +
                         "StatusMessage = @StatusMessage, " +
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
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                hardcodedGroupUsers.Remove(id);
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
                hardcodedGroupUsers.Remove(id);
                return;
            }
            string sql = "DELETE FROM GroupUser WHERE Id = @Id";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }
}