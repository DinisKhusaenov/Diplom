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
    }

    public void Dispose()
    {
        _character.JoinClicked -= OnJoinClicked;
    }

    private void OnJoinClicked() => _joinRequestView.Show();
}
