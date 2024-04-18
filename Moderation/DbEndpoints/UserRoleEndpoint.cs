using Microsoft.Data.SqlClient;
using Moderation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.DbEndpoints
{
    internal class UserRoleEndpoint
    {
        private static string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static void CreateUserRole(Role userRole)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertVoteSql = "INSERT INTO UserRole (RoleId, Name) " +
                                       "VALUES (@RoleId, @Name)";

                using (SqlCommand command = new SqlCommand(insertVoteSql, connection))
                {
                    command.Parameters.AddWithValue("@RoleId", userRole.Id);
                    command.Parameters.AddWithValue("@Name", userRole.Name);

                    command.ExecuteNonQuery();
                }
            }
        }
        public static List<Role> ReadAllUserRoles()
        {
            List<Role> roles = new List<Role>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT RoleId, Name, FROM UserRole";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guid roleId = reader.GetGuid(0);
                            string name = reader.GetString(1);

                            Role role = new Role(roleId, name);
                            roles.Add(role);
                        }
                    }
                }
            }

            return roles;
        }
        public static void UpdateUserRole(Role userRole)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE Role" +
                             "SET Name = @Name";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", userRole.Name);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteUserRole(Role userRole)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteUserRoleSql = "DELETE FROM UserRole" +
                                       "WHERE RoleId = @RoleId";

                using (SqlCommand command = new SqlCommand(deleteUserRoleSql, connection))
                {
                    command.Parameters.AddWithValue("@RoleId", userRole.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}