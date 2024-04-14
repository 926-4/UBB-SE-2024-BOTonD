using Moderation.CurrentSessionNamespace;
using Moderation.Entities;
using Moderation.Repository;
namespace Moderation.Authentication
{
    public class AuthenticationModule(Dictionary<Guid, string> creds, UserRepository users,TimeSpan validityTimeSpanForLogIn)
    {
        private readonly TimeSpan timeout = validityTimeSpanForLogIn;
        private Dictionary<Guid, string> UserIDToPasswordMap { get; set; } = creds;
        private UserRepository userRepo = users;
        public AuthenticationModule(Dictionary<Guid, string> creds, UserRepository users) : this(creds,users, TimeSpan.FromSeconds(10)) { }

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
            LogIn(userRepo.Get(id.Value));
        }
        public void LogIn(User user)
        {
            CurrentSession.GetInstance().user = user;
            CurrentSession.GetInstance().LoginTime = DateTime.Now;
        }
        private static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            new Random().NextBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

    }
}
