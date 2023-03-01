using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize = 10;
    public float minSpawnDistance = 5f;
    public float maxSpawnDistance = 15f;

    public List<GameObject> enemyPool = new List<GameObject>();
    private int enemyCount = 0;

    private void Start()
    {
        // Create a pool of enemies
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }

        // Spawn 10 enemies initially
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy();
        }
    }

    private void Update()
    {
        // Check if an enemy has been destroyed and respawn a new enemy if necessary
        if (enemyCount < poolSize)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = GetEnemy();
        if (enemy != null)
        {
            enemy.SetActive(true);
            enemyCount++;
        }
    }

    public GameObject GetEnemy()
    {
        // Find the first inactive enemy in the pool and return it
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (!enemyPool[i].activeInHierarchy)
            {
                return enemyPool[i];
            }
        }
        // If no inactive enemy is found, return null
        return null;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        // Deactivate the enemy and move it to a new spawn position before returning it to the pool
        enemy.SetActive(false);
        enemy.transform.position = GetRandomSpawnPosition();
        enemyCount--;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Get a random position on the map, away from the player
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 spawnDirection = Random.onUnitSphere;
        spawnDirection.y = 0f;
        float spawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        return playerPosition + spawnDirection.normalized * spawnDistance;
    }
}
