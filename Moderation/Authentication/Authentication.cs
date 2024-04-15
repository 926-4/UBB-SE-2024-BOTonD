using Moderation.CurrentSessionNamespace;
using Moderation.Entities;
using Moderation.Repository;
namespace Moderation.Authentication
{
    public class AuthenticationModule(Dictionary<Guid, string> guidToUsername, UserRepository users/*, TimeSpan validityTimeSpanForLogIn*/)
    {
        //private readonly TimeSpan timeout = validityTimeSpanForLogIn;
        private Dictionary<Guid, string> UserIDToPasswordMap { get; set; } = guidToUsername;
        private readonly UserRepository userRepo = users;

        public void AuthMethod(string username, string password)
        {
            Guid? id = userRepo.GetGuidByName(username);
            if(!id.HasValue)
            {
                throw new ArgumentException($"Username does not exist");
            }
            string salt = GenerateSalt();
            if (salt + UserIDToPasswordMap[id.Value] != salt + password)
            {
                throw new ArgumentException($"Wrong password");
            }
            User? maybeUserAccount = userRepo.Get(id.Value);
            if (maybeUserAccount != null)
            {
                User userAccount = maybeUserAccount;
                LogIn(userAccount);
            }
        }
        public static void LogIn(User user)
        {
            CurrentSession.GetInstance().LogIn(user);
        }
        private static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            new Random().NextBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

    }
}
