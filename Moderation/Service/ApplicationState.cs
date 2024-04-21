﻿using Moderation.Authentication;
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