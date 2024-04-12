using System.Net;
using System.Runtime.CompilerServices;

namespace Moderation.Authentication
{
    public class AuthenticationModule
    {
        private Dictionary<string, DateTime> usersLastLogin = new Dictionary<string, DateTime>();
        private TimeSpan timeout;
        private Dictionary<string, string> credentials;
        public AuthenticationModule(Dictionary<string, string> creds) : this(creds, TimeSpan.FromSeconds(10)) { }
        public AuthenticationModule(Dictionary<string, string> creds, TimeSpan tm)
        {
            credentials = creds;
            timeout = tm;
        }
        public void AuthMethod(string user, string pass)
        {
            if (!credentials.ContainsKey(user))
            {
                throw new ArgumentException($"User {user} does not exist");
            }
            string salt = generateSalt();
            if (salt + credentials[user] != salt + pass)
            {
                throw new ArgumentException($"Wrong password");
            }
            if (isUserLoggedIn(user))
            {
                throw new ArgumentException($"User {user} already logged in");
            }
            LogIn(user);
        }
        public void LogIn(string user)
        {
            if (!usersLastLogin.ContainsKey(user))
            {
                usersLastLogin.Add(user, DateTime.Now);
            }
            else
            {
                usersLastLogin[user] = DateTime.Now;
            }
        }
        private string generateSalt()
        {
            byte[] saltBytes = new byte[16];
            new Random().NextBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
        public bool isUserLoggedIn(string user)
        {
            if (!usersLastLogin.ContainsKey(user))
            {
                return false;
            }
            return DateTime.Now - usersLastLogin[user] <= timeout;
        }

    }
}
