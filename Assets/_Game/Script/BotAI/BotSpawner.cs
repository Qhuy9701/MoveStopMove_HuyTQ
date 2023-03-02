using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnDistance = 5f;
    [SerializeField] private float maxSpawnDistance = 15f;
    [SerializeField] private int spawnBatchSize = 10;
    [SerializeField] private int maxSpawnedBots = 100;
    private List<GameObject> spawnedBots = new List<GameObject>();

    private void Start()
    {
        // Tạo batch đầu tiên của bots
        SpawnBots();
    }

    private void SpawnBots()
    {
        for (int i = 0; i < spawnBatchSize; i++)
        {
            // Lấy một bot từ pool
            GameObject bot = EnemyPool.instance.GetPoolObject();

            // Kiểm tra xem pool còn đủ bot không
            if (bot == null)
            {
                return;
                Debug.Log("Pool is empty");
            }

            // Đặt vị trí của bot ngẫu nhiên xung quanh người chơi
            bot.transform.position = GetRandomSpawnPosition();
            bot.SetActive(true);

            // Thêm bot vào danh sách các bot đã được tạo ra
            spawnedBots.Add(bot);
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

    // Trả lại bot vào pool
    bot.SetActive(false);
    Debug.Log("Bot returned to pool");
    EnemyPool.instance.ReturnPoolObject(bot);

    // Kiểm tra xem pool có đủ bot không, nếu không thì tạo mới
    if (spawnedBots.Count < spawnBatchSize)
    {
        GameObject newBot = EnemyPool.instance.GetPoolObject();
        if (newBot != null)
        {
            newBot.transform.position = GetRandomSpawnPosition();
            newBot.SetActive(true);
            spawnedBots.Add(newBot);

        }
    }
}


    private void OnDrawGizmosSelected()
    {
        // Vẽ quả cầu xung quanh người chơi để hiển thị khoảng cách sinh bot
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxSpawnDistance);
    }
}
