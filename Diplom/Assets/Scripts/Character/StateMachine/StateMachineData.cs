using System;
using UnityEngine;

public class StateMachineData
{
    public float YVelocity;

    private float _speed;
    private Vector2 _input;

    public Vector2 Input
    {
        get => _input;
        set
        {
            if (value.magnitude > 2)
                throw new ArgumentOutOfRangeException(nameof(value));

            _input = value;
        }
    }

    public float Speed
    {
        get => _speed;
        set
        {
            if(value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _speed = value;
        }
    }
}
