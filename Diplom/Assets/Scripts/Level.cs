using Photon.Pun;
using UnityEngine;

public class Level : MonoBehaviourPunCallbacks
{
    [SerializeField] private Character _character;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private CameraSetup _camera;

    private void Awake()
    {
        var character = PhotonNetwork.Instantiate(_character.name, _spawnPosition.position, Quaternion.identity);
        _camera.Initialize(character.transform);
    }
}
