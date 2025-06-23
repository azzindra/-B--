using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public float spawnRate = 1.5f;
    public float xRange = 2.5f;
    public float spawnY = 6f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnNPC), 1f, spawnRate);
    }

    void SpawnNPC()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), spawnY, 0);
        Instantiate(npcPrefab, spawnPos, Quaternion.identity);
    }
}
