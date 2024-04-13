using Moderation.Authentication;
using Moderation.Repository;
using Moderation.SessionManagerNamespace;
namespace Moderation;

public partial class LoginPage : ContentPage
{
    private AuthenticationModule auth;
    public LoginPage()
    {
        auth = new AuthenticationModule(new Dictionary<string, string> {
            {"a","a" },
            { "Victor", "Victor" }, 
            { "Cipri", "Cipri" }, 
            { "Ioan", "Ioan" }, 
            { "Boti", "Boti" }, 
            { "Norby", "Norby" } }, TimeSpan.FromSeconds(5));
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;

        try
        {
            auth.AuthMethod(username, password);
            await Navigation.PushAsync(new MainPage(new SessionManager(username))); 
        }
        catch (ArgumentException ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}