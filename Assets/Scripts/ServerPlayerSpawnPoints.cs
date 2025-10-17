using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ServerPlayerSpawnPoints : MonoBehaviour
{
    public static ServerPlayerSpawnPoints instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    [SerializeField]
    private List<GameObject> spawnPoints;
    public GameObject GetRandomSpawnPoint()
    {
        if (spawnPoints.Count == 0)
            return null;
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }
}
