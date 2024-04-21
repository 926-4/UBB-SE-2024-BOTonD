using Moderation.Authentication;
using Moderation.CurrentSessionNamespace;
using Moderation.Entities;
using Moderation.Entities.Post;
using Moderation.GroupEntryForm;
using Moderation.GroupRulesView;
using Moderation.Repository;
using Question = Moderation.GroupEntryForm.Question;
//using System.Data;

namespace Moderation.Serivce
{
    class ApplicationState
    {
        static private ApplicationState? instance;
        public CurrentSession CurrentSession { get; } = CurrentSession.GetInstance();
        public AuthenticationModule Authenticator { get; } = new AuthenticationModule();
        public GroupRepository Groups { get; } = new();
        public UserRepository UserRepository { get; } = new();
        public Repository<IPost> Posts { get; } = new();
        public AwardRepository Awards { get; } = new();
        public GroupRules Rules { get; } = new();
        public GroupUserRepository GroupUsers { get; } = new();
        public JoinRequestAnswerForOneQuestionRepository JoinRequestForOneQuestionAnswers { get; } = new();
        public JoinRequestRepository JoinRequests { get; } = new();
        public QuestionRepository Questions { get; } = new();
        public ReportRepository Reports { get; } = new();
        public RoleRepository Roles { get; } = new();
        public TextPostRepository TextPosts { get; } = new();
        public VoteRepository Votes { get; } = new();
        static public ApplicationState Get()
        {
            instance ??= new ApplicationState();
            return instance;
        }

    }
}