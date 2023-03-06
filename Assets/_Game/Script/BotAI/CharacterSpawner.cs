using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CharacterSpawner : MonoBehaviour
{
[SerializeField] private float minSpawnDistance = 5f;
[SerializeField] private float maxSpawnDistance = 15f;
[SerializeField] private int spawnBatchSize = 10;
[SerializeField] private float respawnDelay = 2f;
public List<GameObject> spawnedBots = new List<GameObject>();
GameManager gameManager;

private void Awake()
{
    gameManager = GameManager.Instance;
}

private void Start()
{
    SpawnPlayer();
    SpawnBots();
}

public void SpawnPlayer()
{
    GameObject player = ObjectPool.Instance.SpawnFromPool("Player", GetRandomSpawnPosition(), Quaternion.identity);
    if (player == null)
    {
        Debug.LogWarning("Không thể tạo player mới vì player pool đã hết");
        return;
    }
    player.transform.position = GetRandomSpawnPosition();
    player.SetActive(true);
    player.name = "Player";
    spawnedBots.Add(player);
    gameManager.AddCharacter(player);
}

public void SpawnBots()
{
    for (int i = 0; i < spawnBatchSize; i++)
    {
        GameObject newBot = ObjectPool.Instance.SpawnFromPool("Bot", GetRandomSpawnPosition(), Quaternion.identity);
        if (newBot == null)
        {
            Debug.LogWarning("Không thể tạo bot mới vì bot pool đã hết");
            return;
        }
        newBot.transform.position = GetRandomSpawnPosition();
        newBot.SetActive(true);
        newBot.name = "Bot " + i;
        spawnedBots.Add(newBot);
        gameManager.AddCharacter(newBot);
    }
}

public Vector3 GetRandomSpawnPosition()
{
    Vector3 playerPosition = transform.position;
    Vector3 spawnDirection = Random.onUnitSphere;
    spawnDirection.y = 0f;
    float spawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
    return playerPosition + spawnDirection.normalized * spawnDistance;
}

private void OnCollisionEnter(Collision collision)
{
    GameObject collidedObject = collision.gameObject;
    if (collidedObject.CompareTag("Bot"))
    {
        RemoveBot(collidedObject);
    }
}

public void RemoveBot(GameObject bot)
{
    spawnedBots.Remove(bot);
    gameManager.RemoveCharacter(bot);
    bot.SetActive(false);
    StartCoroutine(ReactivateBotAfterDelay(bot, respawnDelay));
}

private IEnumerator ReactivateBotAfterDelay(GameObject bot, float delay)
{
    yield return new WaitForSeconds(delay);
    
    bot.transform.position = GetRandomSpawnPosition();
    bot.SetActive(true);
    spawnedBots.Add(bot);
    gameManager.AddCharacter(bot);
}

private void OnDrawGizmosSelected()
{
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, maxSpawnDistance);
}

//take damagea
}