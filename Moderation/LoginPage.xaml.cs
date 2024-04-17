using Moderation.Authentication;
using Moderation.CurrentSessionNamespace;
using Moderation.Repository;
using Moderation.Entities;
using Moderation.Model;
namespace Moderation;

public partial class LoginPage : ContentPage
{
    private readonly ApplicationState CurrentApp = ApplicationState.Get();
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text.Trim();
        string password = passwordEntry.Text.Trim();

        try
        {
            CurrentApp.Authenticator.AuthMethod(username, password);
            usernameEntry.Text = "";
            passwordEntry.Text = "";
            await Navigation.PushAsync(new MainPage());
        }
        catch (ArgumentException argEx)
        {
            await DisplayAlert("Error", argEx.Message, "OK");
        }
    }
    private void OnQuitClicked(object sender, EventArgs e) => Application.Current?.Quit();
}