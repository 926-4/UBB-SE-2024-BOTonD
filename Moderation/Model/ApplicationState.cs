using Moderation.Authentication;
using Moderation.CurrentSessionNamespace;
using Moderation.Entities;
using Moderation.Entities.Post;
using Moderation.GroupEntryForm;
using Moderation.GroupRulesView;
using Moderation.Repository;
using Question = Moderation.GroupEntryForm.Question;
//using System.Data;

namespace Moderation.Model
{
    class ApplicationState
    {
        static private ApplicationState? instance;
        public CurrentSession CurrentSession { get; } = CurrentSession.GetInstance();
        public AuthenticationModule Authenticator { get; } = new AuthenticationModule();
        public Repository<Group> Groups { get; } = new();
        public UserRepository UserRepository { get; } = new();
        public Repository<IPost> Posts { get; } = new();
        private ApplicationState InitialiseUsers()
        {
            return this;
        }
        private ApplicationState InitialiseGroups(List<User> users)
        {
            return this;
        }
        static public ApplicationState Get()
        {
            instance ??= new ApplicationState()
                .InitialiseUsers();
            return instance;
        }

    }
}
