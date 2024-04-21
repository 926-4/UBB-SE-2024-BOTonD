using Moderation.Entities;
using Moderation.Model;
namespace Moderation.CurrentSessionNamespace
{
    public class CurrentSession
    {
        private static CurrentSession? instance;

        private CurrentSession()
        {
        }

        public static CurrentSession GetInstance()
        {
            instance ??= new CurrentSession();
            return instance;
        }

        public User? User { get; set; }
        public Group? Group { get; set; }
        public DateTime? LoginTime { get; set; }

        public void LogOut()
        {
            User = null;
            LoginTime = null;
        }
        public void LogIn(User user)
        {
            User = user;
            LoginTime = DateTime.Now;
        }
        public void LookIntoGroup(Group group)
        {
            Group = group;
        }
        public void LookOutsideCurrentGroup()
        {
            Group = null;
        }
    }
}