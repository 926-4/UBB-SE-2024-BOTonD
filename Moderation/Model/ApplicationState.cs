using Moderation.Authentication;
using Moderation.CurrentSessionNamespace;
using Moderation.Entities;
using Moderation.Entities.Post;
using Moderation.GroupEntryForm;
using Moderation.GroupRulesView;
using Moderation.Repository;
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
            return InitialiseGroups([victor,boti]);
        }
        private ApplicationState InitialiseGroups(List<User> users)
        {
            Random rand = new();
            var randomUserIndex = rand.Next(users.Count);
            
            Group group = new("Jimmy's borthday party", "None", users[randomUserIndex]);
            Question q1 = new TextQuestion("What presend do you want to give me?");
            Question q2 = new SliderQuestion("How much do you like me?:->", 0, 10);
            Question q3 = new RadioQuestion("What do you want to eat?", ["grill", "pizza", "nothing", "vegetarian"]);
            
            Rule r1 = new("I do solemnly swear to be cool and chill");
            Rule r2 = new("I do solemnly swear to be cool and chill");
            Rule r3 = new("I do solemnly swear I won't ask why the first rule is written twice");
            
            group.GroupEntryQuestions.Add(q1.Id, q1);
            group.GroupEntryQuestions.Add(q2.Id, q2); 
            group.GroupEntryQuestions.Add(q3.Id, q3);
            group.GroupRules.Add(r1.Id, r1);
            group.GroupRules.Add(r2.Id, r2);
            group.GroupRules.Add(r3.Id, r3);
            Groups.Add(group.Id, group);
            
            randomUserIndex = rand.Next(users.Count);
            group = new("Study session @ Jacks", "None", users[randomUserIndex]);
            q1 = new TextQuestion("What time are you free at?");
            q2 = new RadioQuestion("What are you studying?", ["biology", "maths", "physics", "literature"]);
            q3 = new TextQuestion("Any other mentions? (leave free if all's good)");
            r1 = new("I will do my best to help others during my stay here");
            r2 = new("I will not throw a spontaneous party");

            group.GroupEntryQuestions.Add(q1.Id, q1);
            group.GroupEntryQuestions.Add(q2.Id, q2);
            group.GroupEntryQuestions.Add(q3.Id, q3);
            group.GroupRules.Add(r1.Id, r1);
            group.GroupRules.Add(r2.Id, r2);
            Groups.Add(group.Id, group);
            
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
