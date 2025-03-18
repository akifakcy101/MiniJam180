using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    public int humanCount;
    public GameObject humanPrefab;
    public GameObject characters;
    public List<Transform> humanSpawnPoints;
    private Transform spawnPoint;

    void Start()
    {
        humanCount = characters.transform.childCount;
    }

    void Update()
    {
        if (humanCount <= 3)
        {
            spawnPoint = humanSpawnPoints[Random.Range(0, humanSpawnPoints.Count)];
            Instantiate(humanPrefab, spawnPoint.position, humanPrefab.transform.rotation);
            humanCount++;
        }
    }
}
