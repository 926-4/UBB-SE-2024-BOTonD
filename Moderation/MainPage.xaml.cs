using Moderation.GroupEntryForm;
using Moderation.CurrentSessionNamespace;

namespace Moderation
{
    public partial class MainPage : ContentPage
    {
        private static readonly List<string> TeamMembers = ["Boti", "CipriBN", "Ioan", "ahdjff", "Victor"];
        int index = -1;

        public MainPage()
        {
            InitializeComponent();
            HelloLabel.Text = $"Hello {CurrentSession.GetInstance().User?.Username}!";
            LastLoginLabel.Text = $"You logged in at {CurrentSession.GetInstance().LoginTime}.";
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            index++;
            index %= TeamMembers.Count;
            CounterBtn.Text = $"Hello {TeamMembers[index]} !";
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
        private void OnJoinGroupClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GroupEntryForm.GroupEntryForm([
                    new TextQuestion("Hey how are you?"), 
                    new SliderQuestion("How good do you feel today?", 0 , 10),
                    new TextQuestion("What is your name?"),
                    new TextQuestion("Why do you want to join this group?"),
                    new SliderQuestion("How much do you want to join this group?", 0, 1),
                    new TextQuestion("Extra question???")
                ]));
        }
       private void OnLogOutClicked(object sender, EventArgs e)
        {
            CurrentSession.GetInstance().LogOut();
            Navigation.PopAsync();
        }
    }

}
