using System.Collections.Generic;
using UnityEngine;

public class PlayersCollector : MonoBehaviour
{
    public static List<Transform> Players = new List<Transform>();

    private void Start()
    {
        Players.Add(transform);
    }
}
