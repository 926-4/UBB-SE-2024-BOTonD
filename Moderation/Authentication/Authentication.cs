namespace Moderation.Authentication
{
    public class AuthenticationModule(Dictionary<string, string> creds, TimeSpan validityTimeSpanForLogIn)
    {
        private Dictionary<string, DateTime> LastLogInTimePerAccount { get; set; } = [];
        private readonly TimeSpan timeout = validityTimeSpanForLogIn;
        private Dictionary<string, string> AccountNameToPasswordMap { get; set; } = creds;
        public AuthenticationModule(Dictionary<string, string> creds) : this(creds, TimeSpan.FromSeconds(10)) { }

        public void AuthMethod(string username, string password)
        {
            if (!AccountNameToPasswordMap.TryGetValue(username, out string? value))
            {
                throw new ArgumentException($"User {username} does not exist");
            }
            string salt = AuthenticationModule.GenerateSalt();
            if (salt + value != salt + password)
            {
                throw new ArgumentException($"Wrong password");
            }
            if (IsUserLoggedIn(username))
            {
                throw new ArgumentException($"User {username} already logged in");
            }
            LogIn(username);
        }
        public void LogIn(string user)
        {
            if (!LastLogInTimePerAccount.ContainsKey(user))
            {
                LastLogInTimePerAccount.Add(user, DateTime.Now);
            }
            else
            {
                LastLogInTimePerAccount[user] = DateTime.Now;
            }
        }
        private static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            new Random().NextBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
        public bool IsUserLoggedIn(string user)
        {
            if (!LastLogInTimePerAccount.TryGetValue(user, out DateTime value))
            {
                return false;
            }
            return DateTime.Now - value <= timeout;
        }

    }
}
