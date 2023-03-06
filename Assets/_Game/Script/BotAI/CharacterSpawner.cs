using UnityEngine;
using System.Collections.Generic;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnDistance = 5f;
    [SerializeField] private float maxSpawnDistance = 15f;
    [SerializeField] private int spawnBatchSize = 10;   
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
            //multiobject pool
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

    private Vector3 GetRandomSpawnPosition()
    {
        // Nhận một vị trí ngẫu nhiên trên bản đồ, cách xa người chơi
        Vector3 playerPosition = transform.position;
        Vector3 spawnDirection = Random.onUnitSphere;
        spawnDirection.y = 0f;
        float spawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        return playerPosition + spawnDirection.normalized * spawnDistance;
    }

    public void RemoveBot(GameObject bot)
    {
        // Xóa bot khỏi danh sách các bot đã được tạo ra
        spawnedBots.Remove(bot);
        gameManager.RemoveCharacter(bot);   

        // Trả lại bot vào pool
        bot.SetActive(false);
        ObjectPool.Instance.ReturnToPool("Enemy", bot);

        // Kiểm tra xem pool có đủ bot không, nếu không thì tạo mới
        if (spawnedBots.Count < spawnBatchSize)
        {
            GameObject newBot = ObjectPool.Instance.SpawnFromPool("Enemy", GetRandomSpawnPosition(), Quaternion.identity);
            if (newBot == null)
            {
                Debug.LogWarning("Không thể tạo bot mới vì bot pool đã hết");
                return;
            }
            newBot.transform.position = GetRandomSpawnPosition();
            newBot.SetActive(true);
            spawnedBots.Add(newBot);
            gameManager.AddCharacter(newBot);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Vẽ quả cầu xung quanh người chơi để hiển thị khoảng cách sinh bot
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxSpawnDistance);
    }
}