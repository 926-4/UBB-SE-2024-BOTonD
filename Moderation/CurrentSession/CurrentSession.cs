using Moderation.Entities;
namespace Moderation.CurrentSessionNamespace
{
    public class CurrentSession
    {
        private static CurrentSession instance;

        private CurrentSession()
        {
        }

        public static CurrentSession GetInstance()
        {
            if (instance == null)
            {
                instance = new CurrentSession();
            }
            return instance;
        }

        public User? user { get; set; }
        public DateTime LoginTime { get; set; }

        public void Logout()
        {
            user = null;
            LoginTime = DateTime.MinValue;
        }
    }
}
