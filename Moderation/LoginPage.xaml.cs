namespace Moderation;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private void OnLoginClicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;

        if (IsValidCredentials(username, password))
        {
            Navigation.PushAsync(new MainPage());
        }
        else
        {
            DisplayAlert("Error", "Invalid username or password.", "OK");
        }
    }
    private bool IsValidCredentials(string username, string password)
    {
        return true; 
    }
}