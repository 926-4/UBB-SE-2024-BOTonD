using Moderation.Authentication;
using Moderation.SessionManagerNamespace;
namespace Moderation;

public partial class LoginPage : ContentPage
{
    private readonly AuthenticationModule authenticator;
    public LoginPage()
    {
        authenticator = new AuthenticationModule(new Dictionary<string, string> {
            {"a","a" },
            { "Victor", "Victor" }, 
            { "Cipri", "Cipri" }, 
            { "Ioan", "Ioan" }, 
            { "Boti", "Boti" }, 
            { "Norby", "Norby" } }, TimeSpan.FromMinutes(15));
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;

        try
        {
            authenticator.AuthMethod(username, password);
            usernameEntry.Text = "";
            passwordEntry.Text = "";
            await Navigation.PushAsync(new MainPage(new SessionManager(username)));
        }
        catch (ArgumentException argEx)
        {
            await DisplayAlert("Error", argEx.Message, "OK");
        }
    }
    private void OnQuitClicked(object sender, EventArgs e) => Application.Current.Quit();
}