using Moderation.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.DbEndpoints
{
    public class AwardEndpoint
    {
        private static string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreateAward(Award award)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Award VALUES (@Id,@Type)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", award.awardId);
                    command.Parameters.AddWithValue("@Type", award.awardType);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static List<Award> ReadAward()
        {
            List<Award> awards = new List<Award>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Award";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Award award = new Award
                            {
                                awardId = reader.GetGuid(0),
                                awardType = (Award.AwardType)Enum.Parse(typeof(Award.AwardType), reader.GetString(1)),
                               
                            };
                            awards.Add(award);
                        }
                    }
                }
            }
            return awards;
        }
        public static void UpdateAward(Award award)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE Award SET Type=@T WHERE AwardId=@Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", award.awardId);
                    command.Parameters.AddWithValue("@T", award.awardType);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteAward(Award award)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Award WHERE AwardId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", award.awardId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
