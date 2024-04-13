namespace Moderation.SessionManagerNamespace
{
    public class SessionManager(string username)
    {
        public string Username { get; } = username;
        public DateTime LoginTime { get; } = DateTime.Now;
    }
}
