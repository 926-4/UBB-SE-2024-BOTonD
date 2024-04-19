using Microsoft.Data.SqlClient;
using Moderation.Entities;
using System.Configuration;

namespace Moderation.DbEndpoints
{
    internal class UserRoleEndpoint
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public static void CreateUserRole(Role userRole)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string insertVoteSql = "INSERT INTO UserRole (RoleId, Name) " +
                                   "VALUES (@RoleId, @Name)";

            using SqlCommand command = new(insertVoteSql, connection);
            command.Parameters.AddWithValue("@RoleId", userRole.Id);
            command.Parameters.AddWithValue("@Name", userRole.Name);

            command.ExecuteNonQuery();
        }
        public static List<Role> ReadAllUserRoles()
        {
            List<Role> roles = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string sql = "SELECT RoleId, Name, FROM UserRole";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Guid roleId = reader.GetGuid(0);
                    string name = reader.GetString(1);

                    Role role = new(roleId, name);
                    roles.Add(role);
                }
            }

            return roles;
        }
        public static void UpdateUserRole(Role userRole)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "UPDATE Role" +
                         "SET Name = @Name";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Name", userRole.Name);
            command.ExecuteNonQuery();
        }
        public static void DeleteUserRole(Role userRole)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string deleteUserRoleSql = "DELETE FROM UserRole" +
                                   "WHERE RoleId = @RoleId";

            using SqlCommand command = new(deleteUserRoleSql, connection);
            command.Parameters.AddWithValue("@RoleId", userRole.Id);
            command.ExecuteNonQuery();
        }
    }
}