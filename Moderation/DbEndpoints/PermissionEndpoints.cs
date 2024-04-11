using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Moderation.DbEndpoints
{
    public class Permission
    {
        public string Name { get; set; }
        
    }
    public class PermissionEndpoints
    {
        private static string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreatePermission(Permission permission)
        {
            using ( SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Permission VALUES (@Name)";
                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("name", permission.Name);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static List<Permission> ReadPermission()
        {
            List<Permission> permissions = new List<Permission>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Permission";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Permission permission = new Permission {
                                Name = reader.GetString(1)
                            };
                            permissions.Add(permission);
                        }
                    }
                }
            }
            return permissions;
        }
        public static void UpdatePermission(Permission permission,string oldName)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE Permission SET name=@Name where name=@oldName";
                using( SqlCommand command = new SqlCommand(sql,connection))
                {
                    command.Parameters.AddWithValue("@Name", permission.Name);
                    command.Parameters.AddWithValue("@oldName", oldName);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void DeletePermission(Permission permission)
        {
            using( SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Permission WHERE name=@Name";
                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", permission.Name);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
