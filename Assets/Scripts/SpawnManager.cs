using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private float startSpawnTimer;
    [SerializeField] private float maxSpawnTimer;
    [SerializeField] private float minSpawnTimer;
    [SerializeField] private float spawnPositionX;
    [SerializeField] private float maximumBorderZ;
    [SerializeField] private float minimumBorderZ;

    private Vector3 randomSpawnPosition;

    private float spawnTimer;
    private float randomTimer;

    void Start()
    {
        spawnTimer = startSpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (spawnTimer <= 0)
        {
            randomTimer = Random.Range(minSpawnTimer, maxSpawnTimer);
            spawnTimer = randomTimer;
            CreateEnemy();
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }

    private void CreateEnemy()
    {
        var enemy = Instantiate(enemyPrefab);
        enemy.transform.position = RandomSpawnPosition();
        enemy.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
    }

    private Vector3 RandomSpawnPosition()
    {
        var positionY = 0f;
        var randomZ = Random.Range(minimumBorderZ, maximumBorderZ);

        randomSpawnPosition = new Vector3(spawnPositionX, positionY, randomZ);
        return randomSpawnPosition;
    }
}
