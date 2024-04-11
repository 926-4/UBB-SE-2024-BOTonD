using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.DbEndpoints
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int PostScore { get; set; }
        public string Status {  get; set; }
    }
    public class PostEndpoints
    {
        private static string connectionString = "Server=tcp:iss.database.windows.net,1433;Initial Catalog=iss;Persist Security Info=False;User ID=iss;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static void CreatePost(Post post)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Post VALUES (@Id,@user,@score,@Status)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", post.Id);
                    command.Parameters.AddWithValue("@user", post.UserId);
                    command.Parameters.AddWithValue("@score", post.PostScore);
                    command.Parameters.AddWithValue("@Status", post.Status);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static List<Post> ReadPost()
        {
            List<Post> posts = new List<Post>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Post";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Post post = new Post
                            {
                                Id = reader.GetGuid(0),
                                UserId = reader.GetGuid(1),
                                PostScore = reader.GetInt32(2),
                                Status = reader.GetString(3)
                            };
                            posts.Add(post);
                        }
                    }
                }
            }
            return posts;
        }
        public static void UpdatePost(Post post)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE Post SET userId=@u, postScore=@post, status=@status WHERE postId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@u", post.UserId);
                    command.Parameters.AddWithValue("@postScore", post.PostScore);
                    command.Parameters.AddWithValue("@status", post.Status);
                    command.Parameters.AddWithValue("@id", post.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void DeletePost(Post post)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Post WHERE postId=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", post.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

