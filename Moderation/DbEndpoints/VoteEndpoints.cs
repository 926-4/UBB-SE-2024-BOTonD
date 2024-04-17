using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Moderation.Entities;
namespace Moderation.DbEndpoints
{
    public class VoteEndpoints
    {
        private static string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static void CreateVote(Vote vote)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertVoteSql = "INSERT INTO Vote (VoteId, UserPost, PollId, Options) " +
                                       "VALUES (@VoteId, @UserId, @PollId, @Option)";

                using (SqlCommand command = new SqlCommand(insertVoteSql, connection))
                {
                    command.Parameters.AddWithValue("@VoteId", vote.voteId);
                    command.Parameters.AddWithValue("@UserId", vote.userId);
                    command.Parameters.AddWithValue("@PollId", vote.pollId);
                    command.Parameters.AddWithValue("@Option", vote.option);

                    command.ExecuteNonQuery();
                }
            }
        }
        public static List<Vote> ReadAllVotes()
        {
            List<Vote> votes = new List<Vote>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT VoteId, UserPost, PollId, Options FROM Vote";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guid voteId = reader.GetGuid(0);
                            Guid userId = reader.GetGuid(1);
                            Guid pollId = reader.GetGuid(2);
                            string option = reader.GetString(3);

                            Vote vote = new Vote(voteId, userId, pollId, option);
                            votes.Add(vote);
                        }
                    }
                }
            }

            return votes;
        }

        public static void DeleteVote(Guid voteId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteVoteSql = "DELETE FROM Vote WHERE VoteId = @VoteId";

                using (SqlCommand command = new SqlCommand(deleteVoteSql, connection))
                {
                    command.Parameters.AddWithValue("@VoteId", voteId);
                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
