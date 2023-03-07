using UnityEngine;
using System.Collections.Generic;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private int spawnBatchSize = 10;
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
        GameObject player = ObjectPool.Instance.SpawnFromPool("Player", GetRandomPosition(), Quaternion.identity);
        if (player != null)
        {
            player.SetActive(true);
            AddCharacter(player);
        }
    }

    private void SpawnBots()
    {
        for (int i = 0; i < spawnBatchSize; i++)
        {
            GameObject bot = ObjectPool.Instance.SpawnFromPool("Bot", GetRandomPosition(), Quaternion.identity);
            if (bot != null)
            {
                bot.SetActive(true);
                AddCharacter(bot);
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 position = Vector3.zero;
        bool positionIsValid = false;

        while (!positionIsValid)
        {
            position = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            positionIsValid = true;

            foreach (GameObject character in characters)
            {
                if (Vector3.Distance(position, character.transform.position) < CharacterController.attackRange * 2f)
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
        characters.Remove(character);
    }
}
