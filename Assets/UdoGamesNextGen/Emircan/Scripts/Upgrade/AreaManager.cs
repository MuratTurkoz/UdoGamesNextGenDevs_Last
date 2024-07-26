using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance { get; private set;}

    public SpawnManager spawnManager;

    public GameObject[] AreaLocks;

    private void Awake() {
        Instance = this;
    }

    public void UpgradeArea()
    {
        spawnManager.AddArea();
        spawnManager.initialSpawnCount *= 2;
    }
}
