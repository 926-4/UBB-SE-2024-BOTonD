using Moderation.Authentication;
using Moderation.CurrentSessionNamespace;
using Moderation.Entities;
using Moderation.Entities.Post;
using Moderation.Repository;

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
            /// TODO Move to repository/db
            User victor = new(Guid.NewGuid(), "Victor", 1, 1, new UserStatus(UserRestriction.None, DateTime.Now, "None"));
            string victorsPassword = "a";
            User boti = new(Guid.NewGuid(), "Boti", 1, 1, new UserStatus(UserRestriction.None, DateTime.Now, "None"));
            string botisPassword = "a";
            User norby = new(Guid.NewGuid(), "Norby", 1, 1, new UserStatus(UserRestriction.None, DateTime.Now, "None"));
            string norbysPassword = "a";
            User cipri = new(Guid.NewGuid(), "Cipri", 1, 1, new UserStatus(UserRestriction.None, DateTime.Now, "None"));
            string ciprisPassword = "a"; 
            User ioan = new(Guid.NewGuid(), "Ioan", 1, 1, new UserStatus(UserRestriction.None, DateTime.Now, "None"));
            string ioansPassword = "a";
            Authenticator.AddUser(victor, victorsPassword);
            Authenticator.AddUser(boti, botisPassword);
            Authenticator.AddUser(norby, norbysPassword);
            Authenticator.AddUser(cipri, ciprisPassword);
            Authenticator.AddUser(ioan, ioansPassword);
            return this;
        }
        static public ApplicationState Get()
        {
            instance ??= new ApplicationState().InitialiseUsers();
            return instance;
        }

    }
}
