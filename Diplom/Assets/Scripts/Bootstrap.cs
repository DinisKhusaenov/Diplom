using Photon.Pun;
using UnityEngine;

public class Bootstrap : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameSettings _gameSettings;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameSettings.IsActive == false)
                _gameSettings.Show();
            else
                _gameSettings.Hide();
        }
    }
}
