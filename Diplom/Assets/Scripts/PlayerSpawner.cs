using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner
{
    public PlayerSpawner(List<Transform> spawnPoints, GameObject character)
    {
        int index = character.GetComponent<PhotonView>().OwnerActorNr % spawnPoints.Count;

        character.transform.position = spawnPoints[index].position;
    }
}
