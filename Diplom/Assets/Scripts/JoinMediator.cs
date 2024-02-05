using System;

public class JoinMediator: IDisposable
{
    private JoinRequestView _joinRequestView;
    private JoinActiveView _joinActiveView;
    private IJoinHandler _joinHandler;

    public JoinMediator(JoinRequestView joinRequestView, IJoinHandler joinHandler, JoinActiveView joinActiveView)
    {
        _joinRequestView = joinRequestView;
        _joinHandler = joinHandler;
        _joinActiveView = joinActiveView;

        _joinHandler.JoinPressed += OnJoinClicked;
        _joinRequestView.YesClicked += OnYesClicked;
        _joinHandler.JoinedMe += OnJoinedMe;
        _joinHandler.UnjoinedMe += OnUnjoinedMe;
    }

    public void Dispose()
    {
        _joinHandler.JoinPressed -= OnJoinClicked;
        _joinRequestView.YesClicked -= OnYesClicked;
        _joinHandler.JoinedMe -= OnJoinedMe;
        _joinHandler.UnjoinedMe -= OnUnjoinedMe;
    }

    private void OnJoinClicked() => _joinRequestView.Show();

    private void OnYesClicked() => _joinHandler.OnYesClicked();

    private void OnJoinedMe() => _joinActiveView.Show();
    private void OnUnjoinedMe() => _joinActiveView.Hide();
}
