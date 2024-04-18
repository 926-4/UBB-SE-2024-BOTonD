using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Moderation.GroupRulesView;

namespace Moderation.DbEndpoints
{
    public class RuleEndpoints
    {
        private static string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateRule(Rule rule)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO [Rule] (RuleId,GroupId,Text) VALUES (@Id,@Gid,@Text)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", rule.Id);
                    command.Parameters.AddWithValue("@Gid", rule.GroupId);
                    command.Parameters.AddWithValue("@Text", rule.Text);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static List<Rule> ReadRule()
        {
            List<Rule> rules = new List<Rule>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT RuleId, GroupId, Text FROM [Rule]";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Rule rule = new Rule(reader.GetGuid(0),reader.GetGuid(1),reader.GetString(2));
                            rules.Add(rule);
                        }
                    }
                }
            }
            return rules;
        }
        public static void UpdateRule(Rule rule)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE [Rule] SET Text=@T, GroupId = @G WHERE RuleId=@Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", rule.Id);
                    command.Parameters.AddWithValue("@T", rule.Text);
                    command.Parameters.AddWithValue("@G", rule.GroupId);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteRule(Rule rule)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM [Rule] WHERE RuleId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", rule.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
