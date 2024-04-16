using Moderation.Repository;

namespace Moderation.Model
{
    class ApplicationState
    {
        static private ApplicationState? instance;
        static public ApplicationState GetApp()
        {
            instance ??= new ApplicationState();
            return instance;
        }
        public Repository<Group> Groups { get; } = new();
        public UserRepository UserRepository { get; } = new();
    }
}
