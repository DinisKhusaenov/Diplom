using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpawner
{
    private static Dictionary<Transform, bool> _spawnPoints = new Dictionary<Transform, bool>();

    public PlayerSpawner(List<Transform> spawnPoints, GameObject character)
    {
        _spawnPoints.Clear();

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (!_spawnPoints.ContainsKey(spawnPoint))
                _spawnPoints.Add(spawnPoint, false);
        }

        SpawnPlayer(character);
    }

    private void SpawnPlayer(GameObject character)
    {
        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            if (_spawnPoints.Values.ElementAt(i) == false)
            {
                var key = _spawnPoints.Keys.ElementAt(i);
                character.transform.position = key.transform.position;
                _spawnPoints[key] = true;
                break;
            }
        }
    }
}
