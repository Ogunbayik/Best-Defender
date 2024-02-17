using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private float startSpawnTimer;
    [SerializeField] private float maxSpawnTimer;
    [SerializeField] private float minSpawnTimer;

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
        if(spawnTimer <= 0)
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
    }

    private Vector3 RandomSpawnPosition()
    {
        var positionX = 40f;
        var positionY = 0f;
        var randomZ = Random.Range(15, 25);

        randomSpawnPosition = new Vector3(positionX, positionY, randomZ);
        return randomSpawnPosition;
    }
}
