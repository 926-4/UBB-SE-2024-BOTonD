using Moderation.Authentication;
using Moderation.CurrentSessionNamespace;
using Moderation.Repository;
using Moderation.Entities;
namespace Moderation;

public partial class LoginPage : ContentPage
{
    private readonly AuthenticationModule authenticator;
    public LoginPage()
    {
        User ua = new(Guid.NewGuid(), "ua", 1, 1,new UserStatus(UserRestriction.None, DateTime.Now, "None" ));
        User ub = new(Guid.NewGuid(), "ub", 1, 1, new UserStatus(UserRestriction.None, DateTime.Now, "None"));
        User uc = new(Guid.NewGuid(), "uc", 1, 1, new UserStatus(UserRestriction.None, DateTime.Now, "None"));
        authenticator = new AuthenticationModule(new Dictionary<Guid, string> {
            {ua.Id,"ua" },
            {ub.Id, "ub"},
            {uc.Id, "uc" }
            }, new UserRepository(new Dictionary<Guid, User> { { ua.Id, ua }, { ub.Id, ub }, { uc.Id, uc } }));
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
            await Navigation.PushAsync(new MainPage());
        }
        catch (ArgumentException argEx)
        {
            await DisplayAlert("Error", argEx.Message, "OK");
        }
    }
    private void OnQuitClicked(object sender, EventArgs e) => Application.Current?.Quit();
}