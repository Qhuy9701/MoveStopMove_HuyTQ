using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private int spawnBatchSize = 10;

    public List<GameObject> characters = new List<GameObject>();
    public static CharacterSpawner instance;
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
            GameObject bot = ObjectPool.Instance.SpawnFromPool(Constants.TAG_BOT, GetRandomPosition(), Quaternion.identity);
            if (bot != null)
            {
                bot.SetActive(true);
                AddCharacter(bot);
            }
            bot.name = "Bot" + i;
        }
    }

    public Vector3 GetRandomPosition()
    {
        Vector3 position = Vector3.zero;
        bool positionIsValid = false;

        while (!positionIsValid)
        {
            position = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            positionIsValid = true;

            foreach (GameObject character in characters)
            {
                if (Vector3.Distance(position, character.transform.position) < CharacterController._attackRange * 2f)
                {
                    positionIsValid = false;
                    break;
                }
            }
        }
        return position;
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
