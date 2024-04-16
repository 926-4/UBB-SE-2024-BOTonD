using Moderation.GroupEntryForm;
using Moderation.CurrentSessionNamespace;
using Moderation.GroupFeed;
using Moderation.Entities;
using Moderation.Entities.Post;

namespace Moderation
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            LastLoginLabel.Text = $"You logged in at {CurrentSession.GetInstance().LoginTime}.";
        }
        
        private void OnJoinGroupClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GroupEntryForm.GroupEntryForm([
                    new TextQuestion("Hey how are you?"),
                    new SliderQuestion("How good do you feel today?", 0 , 10),
                    new TextQuestion("What is your name?"),
                    new TextQuestion("Why do you want to join this group?"),
                    new SliderQuestion("How much do you want to join this group?", 0, 1),
                    new TextQuestion("Extra question???"),
                    new RadioQuestion( "favourite farm animal", ["dog", "cat", "mouse", "sheep", "cow", "chicken"])
                ]));
        }

        private void OnViewGroupClicked(object sender, EventArgs e)
        {
            // TODO remove the hardcoded values
            string content1 = "I am Victor.";
            User author1 = new User(System.Guid.NewGuid(), "victor3136", 0, 0, new UserStatus(UserRestriction.None, System.DateTime.Now, "enthusiastic"));

            string content2 = "I am Boti.";
            User author2 = new User(System.Guid.NewGuid(), "SzilagyiBotond", 0, 0, new UserStatus(UserRestriction.None, System.DateTime.Now, "at work"));

            string content3 = "I am Cipri.";
            User author3 = new User(System.Guid.NewGuid(), "Cip", 0, 0, new UserStatus(UserRestriction.None, System.DateTime.Now, "at peace"));

            string content4 = "I am Ioan.";
            User author4 = new User(System.Guid.NewGuid(), "neon1024_", 0, 0, new UserStatus(UserRestriction.None, System.DateTime.Now, "in progress"));

            string content5 = "I am Norbi.";
            User author5 = new User(System.Guid.NewGuid(), "PopNorbert", 0, 0, new UserStatus(UserRestriction.None, System.DateTime.Now, "at the gym"));

            Navigation.PushAsync(new GroupFeedView([
                new TextPost(System.Guid.NewGuid(), content1, author1, 0, "1", new List<Award>{}, false),
                new TextPost(System.Guid.NewGuid(), content2, author2, 0, "2", new List<Award>{}, false),
                new TextPost(System.Guid.NewGuid(), content3, author3, 0, "3", new List<Award>{}, false),
                new TextPost(System.Guid.NewGuid(), content4, author4, 0, "4", new List<Award>{}, false),
                new TextPost(System.Guid.NewGuid(), content5, author5, 0, "5", new List<Award>{}, false)
            ]));
        }

        private void OnLogOutClicked(object sender, EventArgs e)
        {
            CurrentSession.GetInstance().LogOut();
            Navigation.PopAsync();
        }
    }
}