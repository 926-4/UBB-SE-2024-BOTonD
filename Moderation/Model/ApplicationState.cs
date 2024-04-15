using Moderation.Repository;

namespace Moderation.Model
{
    class ApplicationState
    {
        private ApplicationState? instance;
        public ApplicationState GetApp()
        {
            instance ??=new ApplicationState();
            return instance;
        }
        public Repository<Group> Groups { get; } = new();

    }
}
