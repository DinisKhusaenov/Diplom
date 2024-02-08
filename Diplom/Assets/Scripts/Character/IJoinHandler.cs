using System;

public interface IJoinHandler
{
    public event Action Jumped;
    public event Action JoinPressed;
    public event Action UnjoinPressed;
    public event Action YesPressed;
    public event Action JoinedMe;
    public event Action UnjoinedMe;

    bool IsJoined { get; }
    bool IsJoinedMe { get; }

    void OnYesClicked();
    void OnQuitGameClicked();
}
