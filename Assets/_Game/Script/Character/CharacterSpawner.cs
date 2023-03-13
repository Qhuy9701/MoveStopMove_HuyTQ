using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private int spawnBatchSize = 10;
    public List<GameObject> posSpawn = new List<GameObject>();
    public List<GameObject> GetPosSpawn()
    {
        return posSpawn;
    }
    public List<GameObject> characters = new List<GameObject>();
    public List<GameObject> GetCharacters()
    {
        return characters;
    }

    private void Start()
    {
        SpawnPlayer();
        SpawnBots();
    }

    private void SpawnPlayer()
    {
        GameObject player = ObjectPool.Instance.SpawnFromPool(Constants.TAG_PLAYER, GetRandomPosition(), Quaternion.identity);
        if (player != null)
        {
            player.SetActive(true);
            AddCharacter(player);
        }
    }

    public void SpawnBots()
    {
        for (int i = 0; i < spawnBatchSize; i++)
        {
            bool spawned = false;
            while (!spawned) // Keep trying to spawn until a valid position is found
            {
                GameObject spawnPosition = GetRandomSpawnPosition();
                if (!IsPositionOccupied(spawnPosition.transform.position))
                {
                    GameObject bot = ObjectPool.Instance.SpawnFromPool(Constants.TAG_BOT, spawnPosition.transform.position, Quaternion.identity);
                    if (bot != null)
                    {
                        bot.SetActive(true);
                        AddCharacter(bot);
                        bot.name = "Bot" + i;
                        spawned = true;
                    }
                }
            }
        }
    }

    private GameObject GetRandomSpawnPosition()
    {
        // Create a list of spawn positions that do not already have a character
        List<GameObject> emptySpawnPositions = posSpawn.Where(p => !p.GetComponent<SpawnPosController>().isHavePlayer()).ToList();

        // Shuffle the list to randomize the order of spawn positions
        emptySpawnPositions = emptySpawnPositions.OrderBy(x => Random.value).ToList();

        // Select the first available spawn position in the shuffled list
        GameObject spawnPosition = emptySpawnPositions[0];
        return spawnPosition;
    }

    private bool IsPositionOccupied(Vector3 position)
    {
        // Check for colliders within 1 unit of the spawn position
        Collider[] colliders = Physics.OverlapSphere(position, 1f);
        // Check if any colliders are characters
        bool hasCharacter = colliders.Any(c => c.CompareTag(Constants.TAG_PLAYER) || c.CompareTag(Constants.TAG_BOT));
        return hasCharacter;
    }


    public Vector3 GetRandomPosition()
    {
        // Create a list of spawn positions that do not already have a character
        List<GameObject> emptySpawnPositions = posSpawn.Where(p => !p.GetComponent<SpawnPosController>().isHavePlayer()).ToList();

        // Shuffle the list to randomize the order of spawn positions
        emptySpawnPositions = emptySpawnPositions.OrderBy(x => Random.value).ToList();

        // Check if the selected position is too close to other characters, if so keep selecting random position until finding an empty one
        float minDistance = 1.0f;
        Vector3 randomPos = Vector3.zero;
        bool validPosition = false;
        while (!validPosition && emptySpawnPositions.Count > 0)
        {
            // Select the first available spawn position in the shuffled list
            randomPos = emptySpawnPositions[0].transform.position;

            // Check if the selected position is too close to other characters
            if (!characters.Any(c => Vector3.Distance(c.transform.position, randomPos) < minDistance))
            {
                validPosition = true;
            }
            else
            {
                // Remove the selected position from the list of available positions
                emptySpawnPositions.RemoveAt(0);
            }
        }

        return randomPos;
    }

    public void AddCharacter(GameObject character)
    {
        characters.Add(character);
    }

    public void RemoveCharacter(GameObject character)
    {
        StartCoroutine(RespawnCharacter(character));
        characters.Remove(character);
    }

    private IEnumerator RespawnCharacter(GameObject character)
    {
        yield return new WaitForSeconds(5f);
        Vector3 randomPosition = GetRandomPosition();
        character.transform.position = randomPosition;
        character.SetActive(true);
        ObjectPool.Instance.ReturnToPool("Bot", character);
    }
}
