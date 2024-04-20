using Microsoft.Data.SqlClient;
using Moderation.Entities;
using System.Configuration;
namespace Moderation.DbEndpoints
{
    // Probably should be deleted
    public class VoteEndpoints
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public static void CreateVote(Vote vote)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string insertVoteSql = "INSERT INTO Vote (VoteId, UserPost, PollId, Options) " +
                                   "VALUES (@VoteId, @UserId, @PollId, @Option)";

            using SqlCommand command = new(insertVoteSql, connection);
            command.Parameters.AddWithValue("@VoteId", vote.voteId);
            command.Parameters.AddWithValue("@UserId", vote.userId);
            command.Parameters.AddWithValue("@PollId", vote.pollId);
            command.Parameters.AddWithValue("@Option", vote.option);

            command.ExecuteNonQuery();
        }
        public static List<Vote> ReadAllVotes()
        {
            List<Vote> votes = [];

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string sql = "SELECT VoteId, UserPost, PollId, Options FROM Vote";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Guid voteId = reader.GetGuid(0);
                    Guid userId = reader.GetGuid(1);
                    Guid pollId = reader.GetGuid(2);
                    string option = reader.GetString(3);

                    Vote vote = new(voteId, userId, pollId, option);
                    votes.Add(vote);
                }
            }

            return votes;
        }
        public static void UpdateVote(Vote vote)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "UPDATE Vote SET Options=@Options WHERE VoteId=@VoteId ";
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Options", vote.option);
            command.Parameters.AddWithValue("@VoteId", vote.voteId);
            command.ExecuteNonQuery();
        }
        public static void DeleteVote(Guid voteId)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string deleteVoteSql = "DELETE FROM Vote WHERE VoteId = @VoteId";

            using SqlCommand command = new(deleteVoteSql, connection);
            command.Parameters.AddWithValue("@VoteId", voteId);
            command.ExecuteNonQuery();
        }


    }
}
