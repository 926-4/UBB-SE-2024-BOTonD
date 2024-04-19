using Microsoft.Data.SqlClient;
using Moderation.Entities;
using System.Configuration;

namespace Moderation.DbEndpoints
{
    public class AwardEndpoint
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public static void CreateAward(Award award)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "INSERT INTO Award VALUES (@Id,@Type)";
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", award.awardId);
            command.Parameters.AddWithValue("@Type", award.awardType.ToString());
            command.ExecuteNonQuery();
        }
        public static List<Award> ReadAwards()
        {
            List<Award> awards = [];
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Award";
                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Award award = new()
                    {
                        awardId = reader.GetGuid(0),
                        awardType = (Award.AwardType)Enum.Parse(typeof(Award.AwardType), reader.GetString(1)),

                    };
                    awards.Add(award);
                }
            }
            return awards;
        }
        public static void UpdateAward(Award award)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "UPDATE Award SET Type=@T WHERE AwardId=@Id";
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", award.awardId);
            command.Parameters.AddWithValue("@T", award.awardType.ToString());
            command.ExecuteNonQuery();
        }
        public static void DeleteAward(Award award)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "DELETE FROM Award WHERE AwardId=@id";
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@id", award.awardId);
            command.ExecuteNonQuery();
        }
    }
}
