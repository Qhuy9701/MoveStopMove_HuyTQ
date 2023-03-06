using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private BotSpawner botSpawner;

    private GameObject player;

    private void Awake()
    {

    }
}
