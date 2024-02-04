using Photon.Pun;
using UnityEngine;

public class Bootstrap : MonoBehaviourPunCallbacks
{
    [SerializeField] private Character _character;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private CameraSetup _camera;
    [SerializeField] private JoinRequestView _joinRequestView;

    private JoinMediator _joinMediator; 

    private void Awake()
    {
        var character = PhotonNetwork.Instantiate(_character.name, _spawnPosition.position, Quaternion.identity);
        _camera.Initialize(character.transform);
        _joinMediator = new JoinMediator(_joinRequestView, character.GetComponent<Character>());
    }
}
