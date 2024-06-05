using Zenject;

public class ServiceInstaller : MonoInstallerPunCallbacks
{
    public override void InstallBindings()
    {
        BindInput();
    }

    private void BindInput()
    {
        CharacterInput input = new CharacterInput();
        input.Enable();

        Container.Bind<CharacterInput>().FromInstance(input).AsSingle();
    }
}
