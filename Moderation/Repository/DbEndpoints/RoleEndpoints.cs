using Microsoft.Data.SqlClient;
using Moderation.Entities;
using Moderation.Serivce;
using System.Configuration;

namespace Moderation.DbEndpoints
{
    public class RoleEndpoints
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private static readonly Dictionary<Guid, Role> hardcodedRoles = new()
        {
            {
                Guid.Parse("00E25F4D-6C60-456B-92CF-D37751176177"),
                new Role(
                    Guid.Parse("00E25F4D-6C60-456B-92CF-D37751176177"),
                    "Creator",
                    Enum.GetValues(typeof(Permission)).Cast<Permission>().ToList())
            },{
                Guid.Parse("5B4432BD-7A3C-463C-8A4B-34E4BF452AC3"),
                new Role(
                    Guid.Parse("5B4432BD-7A3C-463C-8A4B-34E4BF452AC3"),
                    "Member",
                   [Permission.CreatePost, Permission.ReportPost, Permission.EditOwnPost, Permission.React, Permission.CreateEvent, Permission.EditOwnComment,
                    Permission.InviteFriends, Permission.RemoveOwnComment, Permission.RemoveOwnPost, Permission.RemoveOwnReaction, Permission.UpdateOwnReaction])
            }
        };
        public static void CreateRole(Role role)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                hardcodedRoles.Add(role.Id, role);
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
                hardcodedRoles.Add(role.Id, role);
                return;
            }

            string sql = "INSERT INTO UserRole VALUES (@RoleId,@Name)";
            using (SqlCommand command = new(sql, connection))
            {
                command.Parameters.AddWithValue("@RoleId", role.Id);
                command.Parameters.AddWithValue("@Name", role.Name);
                command.ExecuteNonQuery();
            }
            foreach (var permission in role.Permissions)
            {
                sql = "INSERT INTO RolePermission VALUES (@RoleId,@Permission)";
                using SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@RoleId", role.Id);
                command.Parameters.AddWithValue("@Permission", permission.ToString());
                command.ExecuteNonQuery();
            }
        }
        public static List<Role> ReadRole()
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                return [.. hardcodedRoles.Values];
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
                return [.. hardcodedRoles.Values];
            }
            List<Role> roles = [];
            connection.Open();
            string sql = "SELECT RoleId, Name FROM UserRole";
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
            return roles;
        }
        public static void UpdateRoleName(Guid roleId, string newName)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                if (!hardcodedRoles.TryGetValue(roleId, out Role? toUpdate))
                    return;
                toUpdate.Name = newName;
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
                if (!hardcodedRoles.TryGetValue(roleId, out Role? toUpdate))
                    return;
                toUpdate.Name = newName;
                return;
            }
            connection.Open();

            string sql = "UPDATE UserRole SET Name = @NewName WHERE RoleId = @RoleId";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@NewName", newName);
            command.Parameters.AddWithValue("@RoleId", roleId);

            command.ExecuteNonQuery();
        }
        public static void UpdateRolePermissions(Guid roleId, List<Permission> newPermissions)
        {
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                if (!hardcodedRoles.TryGetValue(roleId, out Role? toUpdate))
                    return;
                toUpdate.Permissions = newPermissions;
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
                if (!hardcodedRoles.TryGetValue(roleId, out Role? toUpdate))
                    return;
                toUpdate.Permissions = newPermissions;
                return;
            }
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
            if (!ApplicationState.Get().DbConnectionIsAvailable)
            {
                hardcodedRoles.Remove(roleId);
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
                hardcodedRoles.Remove(roleId);
                return;
            }
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