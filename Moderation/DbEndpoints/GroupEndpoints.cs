using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Model;
using Moderation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.DbEndpoints
{
    internal class GroupEndpoints
    {
        private static readonly string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static void CreateGroup(Group group)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "INSERT INTO Group (Id, Name, Description, Owner) " +
                         "VALUES (@Id, @Name, @Description, @Owner)";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", group.Id);
            command.Parameters.AddWithValue("@Name", group.Name);
            command.Parameters.AddWithValue("@Description", group.Description);
            command.Parameters.AddWithValue("@Owner", group.Creator.Id);

            command.ExecuteNonQuery();
        }
        public static List<Group> ReadAllGroups()
        {
            List<Group> groups = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string sql = "SELECT Id, Name, Description, Owner FROM User";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var userId = reader.GetGuid(3);
                    string username = ApplicationState.Get().UserRepository.Get(userId).Username;
                    User user = new User(userId, username);

                    Group group = new Group(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), user);
                    groups.Add(group);
                }
            }

            return groups;
        }
        public static void UpdateGroup(Group group)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "UPDATE Group" +
                         "SET Name = @Name, Description = @Description, Owner = @Owner" +
                         "WHERE Id = @Id";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Name", group.Name);
            command.Parameters.AddWithValue("@Description", group.Description);
            command.Parameters.AddWithValue("@Owner", group.Creator.Id);

            command.ExecuteNonQuery();
        }
        public static void DeleteGroup(Guid id)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "DELETE FROM Group WHERE Id = @Id";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }
}