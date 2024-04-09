namespace Moderation
{
    public partial class MainPage : ContentPage
    {
        private static readonly List<string> TeamMembers = ["Boti", "Cipri", "Ioan", "Norby", "Victor"];
        int index = -1;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            index++;
            index %= TeamMembers.Count();
            CounterBtn.Text = $"Hello {TeamMembers[index]} !";
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
