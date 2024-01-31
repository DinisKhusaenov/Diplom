using System;
using UnityEngine;

[Serializable]
public class AirbornStateConfig
{
    [SerializeField, Range(0, 10)] private float _speed;
    [SerializeField] private JumpingStateConfig _jumpingStateConfig;

    public JumpingStateConfig JumpingStateConfig => _jumpingStateConfig;
    public float Speed => _speed;
    public float BaseGravity
        => 2f * _jumpingStateConfig.MaxHeight / (_jumpingStateConfig.TimeToReachMaxHeight * _jumpingStateConfig.TimeToReachMaxHeight);
}
