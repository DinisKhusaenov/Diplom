using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraSetup : MonoBehaviour
{
    private Transform _target;
    private CinemachineVirtualCamera _virtualCamera;

    public void Initialize(Transform target)
    {
        _target = target;

        _virtualCamera = GetComponent<CinemachineVirtualCamera>();

        SetTarget();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TryChangeTarget();
        }
    }

    private void SetTarget()
    {
        if (_target != null)
            SetCameraTarget(_target);
    }

    private void TryChangeTarget()
    {
        foreach (var player in PlayersCollector.Players)
        {
            if (player != null && player != _target)
                SetCameraTarget(player);
        }
    }

    private void SetCameraTarget(Transform target)
    {
        _virtualCamera.Follow = target;
        _virtualCamera.LookAt = target;
    }
}
