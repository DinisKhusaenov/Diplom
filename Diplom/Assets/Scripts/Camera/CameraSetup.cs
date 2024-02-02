using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraSetup : MonoBehaviour
{
    private Transform _target;

    public void Initialize(Transform target)
    {
        _target = target;

        SetTarget();
    }

    private void SetTarget()
    {
        CinemachineVirtualCamera virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (_target != null)
        {
            virtualCamera.Follow = _target;
            virtualCamera.LookAt = _target;
        }
    }
}
