using Moderation.CurrentSessionNamespace;
using Moderation.Entities.Post;
using Moderation.Repository;

namespace Moderation.Model
{
    class ApplicationState
    {
        static private ApplicationState? instance;
        public static CurrentSession CurrentSession { get; } = CurrentSession.GetInstance();
        static public ApplicationState GetApp()
        {
            instance ??= new ApplicationState();
            return instance;
        }
        public Repository<Group> Groups { get; } = new();
        public UserRepository UserRepository { get; } = new();
        public Repository<IPost> Posts { get; } = new();

    }
}
