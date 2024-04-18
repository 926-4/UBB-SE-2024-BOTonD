using Moderation.Authentication;
using Moderation.CurrentSessionNamespace;
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
        public GroupRepository Groups { get; } = new();
        public UserRepository UserRepository { get; } = new();
        public GroupUserRepository GroupUserRepository { get; } = new();
        public Repository<IPost> Posts { get; } = new();
        public Repository<JoinRequest> JoinRequests { get; } = new();
        private ApplicationState InitialiseUsers()
        {
            /// Read data from DB
            return this;
        }
        private ApplicationState InitialiseGroups()
        {
            /// Read data from db
            return this;
        }
        static public ApplicationState Get()
        {
            instance ??= new ApplicationState()
                .InitialiseUsers()
                .InitialiseGroups();
            return instance;
        }

    }
}
