using Moderation.CurrentSessionNamespace;
using Moderation.Entities;
using Moderation.Serivce;
namespace Moderation;

public partial class LoginPage : ContentPage
{
    private readonly ApplicationState CurrentApp = ApplicationState.Get();
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object? sender, EventArgs e)
    {
        string username = usernameEntry.Text.Trim();
        string password = passwordEntry.Text.Trim();

        try
        {
            //CurrentApp.Authenticator.AuthMethod(username, password); <-- this got temporarily replaced by this:|
            //                                                                                                   v
            Guid userId = ApplicationState.Get().UserRepository.GetGuidByName(username)
                ?? throw new ArgumentException("No account with that username");
            User currentUser = ApplicationState.Get().UserRepository.Get(userId)
                ?? throw new ArgumentException("Could not find user");
            string pass = currentUser.Password;

            if (pass != password)
            {
                throw new ArgumentException("Incorrect password");
            }
            usernameEntry.Text = "";
            passwordEntry.Text = "";
            CurrentSession.GetInstance().LogIn(currentUser);
            await Navigation.PushAsync(new GroupsView());
        }
        catch (ArgumentException argEx)
        {
            await DisplayAlert("Error", argEx.Message, "OK");
        }
    }
    private void OnQuitClicked(object sender, EventArgs e) => Application.Current?.Quit();
}