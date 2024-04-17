using Moderation.CurrentSessionNamespace;
using Moderation.Entities;
using Moderation.Entities.Post;
using Moderation.Entities.Report;
using Moderation.GroupFeed;
using Moderation.Model;

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
            Group? currentGroupMaybe = ApplicationState.Get().CurrentSession.Group;
            if (currentGroupMaybe != null)
            {
                Group currentGroup = currentGroupMaybe;
                Navigation.PushAsync(new GroupEntryForm.GroupEntryForm(currentGroup.GroupEntryQuestions.GetAll()));
            }
            //[
            //    new TextQuestion("Hey how are you?"),
            //    new SliderQuestion("How good do you feel today?", 0, 10),
            //    new TextQuestion("What is your name?"),
            //    new TextQuestion("Why do you want to join this group?"),
            //    new SliderQuestion("How much do you want to join this group?", 0, 1),
            //    new TextQuestion("Extra question???"),
            //    new RadioQuestion("favourite farm animal", ["dog", "cat", "mouse", "sheep", "cow", "chicken"])
            //]); ;);
        }

        private void OnViewGroupClicked(object sender, EventArgs e)
        {
            // TODO remove the hardcoded values
            string content1 = "I am Victor. I am Victor. I am Victor. I am Victor. I am Victor. I am Victor. I am Victor. I am Victor. I am Victor. I am Victor. I am Victor. I am Victor. I am Victor. I am Victor. I am Victor. I am Victor.";
            User author1 = new("victor3136");

            string content2 = "I am Boti.";
            User author2 = new("SzilagyiBotond");

            string content3 = "I am Cipri.";
            User author3 = new("Cip");

            string content4 = "I am Ioan.";
            User author4 = new("neon1024_");

            string content5 = "I am Norbi.";
            User author5 = new("PopNorbert");

            Navigation.PushAsync(new GroupFeedView([
                new TextPost(content1, author1, "1"),
                new TextPost(content2, author2, "2"),
                new TextPost(content3, author3, "3"),
                new TextPost(content4, author4, "4"),
                new TextPost(content5, author5, "5")
            ])) ;
        }
        private void OnViewReportsClicked(object sender, EventArgs e)
        {
            User author4 = new("neon1024_");
            User author5 = new("NopPornbert");
            TextPost post = new("AAAA", author5);
            Navigation.PushAsync(new ReportListView.ReportListView([
                new PostReport(author4.Id,"This post is very bad for this community",post.Id),
                new PostReport(author5.Id,"I am reporting my own post!!:-DDD",post.Id)
                ]));
        }
        private void OnRequestsClicked(object sender, EventArgs e)
        {
            User author4 = new("neon1024_");
            User author5 = new("NopPornbert");
            Navigation.PushAsync(new JoinRequestView.JoinRequestListView([
                new JoinRequest(author4.Id,new Dictionary<string, string>{
                    {"How you are" ,"Good"},
                    {"Do you want to join", "Yeah" }
                }),
                new JoinRequest(author5.Id,new Dictionary<string, string>{
                    {"How you are", "Bad" },
                    {"Do you want to join", "Nah"}
                })
                ]));
        }
        private void OnLogOutClicked(object sender, EventArgs e)
        {
            CurrentSession.GetInstance().LogOut();
            Navigation.PopAsync();
        }
    }
}