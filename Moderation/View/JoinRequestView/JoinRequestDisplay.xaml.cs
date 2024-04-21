using Moderation.Entities;
using Moderation.Serivce;

namespace Moderation.JoinRequestView;

public partial class JoinRequestDisplay : ContentView
{
    private JoinRequest joinRequest;
    public JoinRequestDisplay(JoinRequest joinRequest)
    {
        this.joinRequest = joinRequest;
        //InitializeComponent();
        CreateView();
    }
    public void CreateView()
    {
        var stackLayout = new StackLayout { Margin = new Thickness(20) };
        var requestLabel = new Label { Text = "Request", FontSize = 20, FontAttributes = FontAttributes.Bold };
        stackLayout.Children.Add(requestLabel);

        var requestIdStackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
        var requestIdLabel = new Label { Text = "Request ID:", FontSize = 16, Margin = new Thickness(0, 4, 10, 0) };
        var requestIdValueLabel = new Label { Text = joinRequest.Id.ToString(), FontSize = 16, Margin = new Thickness(0, 4, 0, 0) };
        requestIdStackLayout.Children.Add(requestIdLabel);
        requestIdStackLayout.Children.Add(requestIdValueLabel);
        stackLayout.Children.Add(requestIdStackLayout);

        GroupUser? groupUser = ApplicationState.Get().GroupUsers.Get(joinRequest.UserId);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        User? user = ApplicationState.Get().UserRepository.Get(groupUser.UserId);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var userIdStackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
        var userIdLabel = new Label { Text = "User Name:", FontSize = 16, Margin = new Thickness(0, 4, 10, 0) };
        var userIdValueLabel = new Label { Text = user?.Username, FontSize = 16, Margin = new Thickness(0, 4, 0, 0) };

        userIdStackLayout.Children.Add(userIdLabel);
        userIdStackLayout.Children.Add(userIdValueLabel);
        stackLayout.Children.Add(userIdStackLayout);
        IEnumerable<JoinRequestAnswerToOneQuestion> answers = ApplicationState.Get().JoinRequestForOneQuestionAnswers.GetAll().Where(answer => answer.RequestId == joinRequest.Id);

        foreach (var answer in answers)
        {
            var grid = new Grid { Margin = new Thickness(0, 10, 0, 0) };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            var keyLabel = new Label { Text = answer.QuestionText, FontSize = 16 };
            var valueLabel = new Label { Text = answer.QuestionAnswer, FontSize = 16 };

            Grid.SetColumn(keyLabel, 0);
            Grid.SetColumn(valueLabel, 1);

            grid.Children.Add(keyLabel);
            grid.Children.Add(valueLabel);
            grid.Padding = new Thickness(5);

            var frame = new Frame
            {
                BorderColor = Color.Parse("Black"),
                HasShadow = false,
                Padding = 1,
                CornerRadius = 5,
                Content = grid
            };

            stackLayout.Children.Add(frame);
        }



        var buttonsStackLayout = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 10, 0, 0) };
        var acceptButton = new Button { Text = "Accept" };
        acceptButton.Clicked += OnAcceptClicked;
        var declineButton = new Button { Text = "Decline" };
        declineButton.Clicked += OnDeclineClicked;
        buttonsStackLayout.Children.Add(acceptButton);
        buttonsStackLayout.Children.Add(declineButton);
        stackLayout.Children.Add(buttonsStackLayout);
        Content = stackLayout;
    }

    private void OnDeclineClicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnAcceptClicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}