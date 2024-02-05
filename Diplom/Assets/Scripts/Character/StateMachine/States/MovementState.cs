using UnityEngine;

public abstract class MovementState : IState
{
    protected readonly IStateSwitcher StateSwitcher;
    protected readonly StateMachineData Data;
    private const float FullRotation = 360;
    private const float _timeToReachTargetRotation = 0.14f;

    private readonly Character _character;
    private Camera _camera;

    private float _currentTargetRotation;
    private float _dampedTargetRotationCurrentVelocity;
    private float _dampedTargetRotationPassedTime;

    public MovementState(IStateSwitcher stateSwitcher, Character character, StateMachineData data, Camera camera)
    {
        StateSwitcher = stateSwitcher;
        _character = character;
        Data = data;
        _camera = camera;
    }

    protected CharacterInput Input => _character.Input;
    protected CharacterController CharacterController => _character.Controller;
    protected CharacterView View => _character.View;

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        Data.Input = ReadMovementInput();
    }

    public virtual void Update()
    {
        if (_character.IsJoined == true) return;

        Vector2 inputDirection = ReadMovementInput();
        Vector3 convertedDirection = GetConvertedInputDirection(inputDirection);

        float inputAngleDirection = GetDirectionAngleFrom(convertedDirection);
        inputAngleDirection = AddCameraAngleTo(inputAngleDirection);

        CharacterController.Move(new Vector3(0, Data.YVelocity, 0) * Time.deltaTime);

        if (convertedDirection != Vector3.zero)
        {
            Rotate(inputAngleDirection);
            Move(Quaternion.Euler(0, inputAngleDirection, 0) * Vector3.forward);
        }
    }

    protected virtual void AddInputActionsCallbacks() { }

    protected virtual void RemoveInputActionsCallbacks() { }

    protected bool IsHorizontalInputZero() => Data.Input.magnitude == 0;

    private void Move(Vector3 inputDirection)
    {
        float scaledMoveSpeed = GetScaledMoveSpeed();

        Vector3 normalizedInputDirection = inputDirection.normalized;

        CharacterController.Move(normalizedInputDirection * scaledMoveSpeed);
    }

    private void Rotate(float inputAngleDirection)
    {
        if (inputAngleDirection != _currentTargetRotation)
            UpdateTargetRotationData(inputAngleDirection);

        RotateTowardsTargetRotation();
    }

    private void UpdateTargetRotationData(float targetAngle)
    {
        _currentTargetRotation = targetAngle;
        _dampedTargetRotationPassedTime = 0f;
    }

    private void RotateTowardsTargetRotation()
    {
        float currentYAngle = GetCurrentRotationAngle();

        if (currentYAngle == _currentTargetRotation)
            return;

        float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, _currentTargetRotation, ref _dampedTargetRotationCurrentVelocity, _timeToReachTargetRotation - _dampedTargetRotationPassedTime);
        _dampedTargetRotationPassedTime += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0, smoothedYAngle, 0);
        _character.transform.rotation = targetRotation;
    }

    private float GetScaledMoveSpeed() => Data.Speed * Time.deltaTime;

    private float GetCurrentRotationAngle() => _character.transform.rotation.eulerAngles.y;

    private float GetDirectionAngleFrom(Vector3 inputMoveDirection)
    {
        float directionAngle = Mathf.Atan2(inputMoveDirection.x, inputMoveDirection.z) * Mathf.Rad2Deg;

        if (directionAngle < 0)
            directionAngle += FullRotation;

        return directionAngle;
    }

    private float AddCameraAngleTo(float angle)
    {
        angle += _camera.transform.eulerAngles.y;

        if (angle > FullRotation)
            angle -= FullRotation;

        return angle;
    }

    private Vector3 GetConvertedInputDirection(Vector2 direction) => new Vector3(direction.x, 0, direction.y);

    private Vector2 ReadMovementInput() => Input.Movement.Move.ReadValue<Vector2>();

}
