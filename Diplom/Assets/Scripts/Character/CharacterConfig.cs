using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configs/CharaterConfig")]
public class CharacterConfig : ScriptableObject
{
    [SerializeField] private RunningStateConfig _runningStateConfig;
    [SerializeField] private AirbornStateConfig _airbornStateConfig;

    public RunningStateConfig RunningStateConfig => _runningStateConfig;
    public AirbornStateConfig AirbornStateConfig => _airbornStateConfig;
}
