using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private static List<Transform> _players = new List<Transform>();

    public IEnumerable<Transform> Players => _players;

    private void Awake()
    {
        _players.Clear();
        _players.Add(transform);
    }

    public void RemovePlayer()
    {
        _players.Remove(transform);
    }
}
