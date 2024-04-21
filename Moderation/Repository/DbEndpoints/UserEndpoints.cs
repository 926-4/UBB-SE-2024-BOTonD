using Microsoft.Data.SqlClient;
using Moderation.Entities;
using System.Configuration;

namespace Moderation.DbEndpoints
{
    public class UserEndpoints
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private static bool dbConnectionIsAvailable = true;
        /// <summary>
        ///  Azure has a monthly free limit that we went over. The db will be once again available starting May 1st 2024, but in the meantime, 
        ///  use these hardcoded values:
        /// </summary>
        private static readonly List<User> hardcodedUsers = [new User(Guid.Parse("B7CCB450-EE32-4BFF-8383-E0A0F36CAC06"), "victor", "alabala"),
                                                     new User(Guid.Parse("0825D1FD-C40B-4926-A128-2D924D564B3E"), "boti", "ababab"),
                                                     new User(Guid.Parse("E17FF7A1-95DF-4EAE-8A69-9B139CCD7CA8"), "norby", "norb"),
                                                     new User(Guid.Parse("E268B52E-DD82-4D86-AE17-9F8DE883BEFE"), "ioan", "neon"),
                                                     new User(Guid.Parse("E268B52E-DD82-4D86-AE17-9F8DE883BEFE"), "cipri", "bn"),
                                                     new User(Guid.Parse("9EBE3762-1CD6-45BD-AF9F-0D221CB078D1"), "izabella", "yup")];
        public static void CreateUser(User user)
        {
            if (!dbConnectionIsAvailable)
            {
                hardcodedUsers.Add(user);
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
                dbConnectionIsAvailable = false;
                hardcodedUsers.Add(user);
                return;
            }
            string sql = "INSERT INTO [User] (Id, Username, Password) " +
                         "VALUES (@Id, @Username, @Password)";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);

            command.ExecuteNonQuery();
        }

        public static List<User> ReadAllUsers()
        {
            if (!dbConnectionIsAvailable)
            {
                return hardcodedUsers;
            }
            List<User> users = [];
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                dbConnectionIsAvailable = false;
                return hardcodedUsers;
            }
            string sql = "SELECT Id, Username, Password FROM [User]";
            using SqlCommand command = new(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                User user = new(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
                users.Add(user);
            }
            return users;
        }
        private static void UpdateUserIfDBUnavailable(User newValues)
        {
            User? toUpdate = hardcodedUsers.Where(u => u.Id == newValues.Id).First();
            if (toUpdate == null)
                return;
            toUpdate.Username = newValues.Username;
            toUpdate.Password = newValues.Password;
            return;
        }
        public static void UpdateUser(User newValues)
        {
            if (!dbConnectionIsAvailable)
            {
                UpdateUserIfDBUnavailable(newValues);
            }
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                dbConnectionIsAvailable = false;
                UpdateUserIfDBUnavailable(newValues);
                return;
            }
            string sql = "UPDATE User" +
                         "SET Username = @Username, Password = @Password" +
                         "WHERE Id = @Id";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Username", newValues.Username);
            command.Parameters.AddWithValue("@Password", newValues.Password);
            command.Parameters.AddWithValue("@Id", newValues.Id);

            command.ExecuteNonQuery();
        }
        private static void DeleteUserIfDBUnavailable(Guid id)
        {
            User? toRemove = hardcodedUsers.Where(u => u.Id == id).First();
            if (toRemove == null)
                return;
            hardcodedUsers.Remove(toRemove);
            return;
        }
        public static void DeleteUser(Guid id)
        {
            if (!dbConnectionIsAvailable)
            {
                DeleteUserIfDBUnavailable(id);
            }
            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException azureTrialExpired)
            {
                Console.WriteLine(azureTrialExpired.Message);
                dbConnectionIsAvailable = false;
                DeleteUserIfDBUnavailable(id);
                return;
            }

            string sql = "DELETE FROM User WHERE Id = @Id";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }
}