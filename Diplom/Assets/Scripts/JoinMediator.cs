using System;

public class JoinMediator: IDisposable
{
    private JoinRequestView _joinRequestView;
    private Character _character;

    public JoinMediator(JoinRequestView joinRequestView, Character character)
    {
        _joinRequestView = joinRequestView;
        _character = character;

        _character.JoinClicked += OnJoinClicked;
        _joinRequestView.YesClicked += OnYesClicked;
    }

    public void Dispose()
    {
        _character.JoinClicked -= OnJoinClicked;
        _joinRequestView.YesClicked -= OnYesClicked;
    }

    private void OnJoinClicked() => _joinRequestView.Show();

    private void OnYesClicked() => _character.OnYesClicked();
}
