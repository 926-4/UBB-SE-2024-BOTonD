using Microsoft.Data.SqlClient;
using Moderation.Entities;

namespace Moderation.DbEndpoints
{
    public class RoleEndpoints
    {
        private static readonly string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateRole(Role role)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "INSERT INTO UserRole VALUES (@Id,@N)";
            using (SqlCommand command = new(sql, connection))
            {
                command.Parameters.AddWithValue("@Id", role.Id);
                command.Parameters.AddWithValue("@N", role.Name);
                command.ExecuteNonQuery();
            }
            foreach (var permission in role.Permissions)
            {
                sql = "Insert into RolePermission Values (@id,@p)";
                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@Id", role.Id);
                command.Parameters.AddWithValue("@p", permission);
                command.ExecuteNonQuery();
            }
        }
        public static List<Role> ReadRole()
        {
            List<Role> roles = [];
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM UserRole r";
                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Role role = new(
                        reader.GetGuid(0),
                        reader.GetString(1)
                    );

                    // Fetch permissions for the current role from RolePermission table
                    string rolePermissionSql = "SELECT Permission FROM RolePermission WHERE RoleId = @RoleId";
                    using (SqlCommand rolePermissionCommand = new(rolePermissionSql, connection))
                    {
                        rolePermissionCommand.Parameters.AddWithValue("@RoleId", role.Id);

                        using SqlDataReader rolePermissionReader = rolePermissionCommand.ExecuteReader();
                        while (rolePermissionReader.Read())
                        {
                            // Map the permission string to the Permission enum
                            string permissionString = rolePermissionReader.GetString(0);
                            Permission permission = (Permission)Enum.Parse(typeof(Permission), permissionString);
                            role.Permissions.Add(permission);
                        }
                    }
                    roles.Add(role);
                }
            }
            return roles;
        }
        public static void UpdateRoleName(Guid roleId, string newName)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "UPDATE UserRole SET Name = @NewName WHERE RoleId = @RoleId";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@NewName", newName);
            command.Parameters.AddWithValue("@RoleId", roleId);

            command.ExecuteNonQuery();
        }
        public static void UpdateRolePermissions(Guid roleId, List<Permission> newPermissions)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            // Delete existing permissions for the role
            string deleteSql = "DELETE FROM RolePermission WHERE RoleId = @RoleId";
            using (SqlCommand deleteCommand = new(deleteSql, connection))
            {
                deleteCommand.Parameters.AddWithValue("@RoleId", roleId);
                deleteCommand.ExecuteNonQuery();
            }

            // Insert new permissions for the role
            string insertSql = "INSERT INTO RolePermission (RoleId, Permission) VALUES (@RoleId, @Permission)";
            foreach (Permission permission in newPermissions)
            {
                using SqlCommand insertCommand = new(insertSql, connection);
                insertCommand.Parameters.AddWithValue("@RoleId", roleId);
                insertCommand.Parameters.AddWithValue("@Permission", permission.ToString());
                insertCommand.ExecuteNonQuery();
            }
        }

        public static void DeleteRole(Guid roleId)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            // Delete associated permissions from RolePermission table first
            string deletePermissionSql = "DELETE FROM RolePermission WHERE RoleId = @RoleId";
            using (SqlCommand deletePermissionCommand = new(deletePermissionSql, connection))
            {
                deletePermissionCommand.Parameters.AddWithValue("@RoleId", roleId);
                deletePermissionCommand.ExecuteNonQuery();
            }

            // Delete role from UserRole table after deleting associated permissions
            string deleteRoleSql = "DELETE FROM UserRole WHERE RoleId = @RoleId";
            using SqlCommand deleteRoleCommand = new(deleteRoleSql, connection);
            deleteRoleCommand.Parameters.AddWithValue("@RoleId", roleId);
            deleteRoleCommand.ExecuteNonQuery();
        }
    }
}

