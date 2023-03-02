using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize = 10;

    private List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            pool.Add(enemy);
        }
    }

    public GameObject GetPoolObject()
    {
        foreach (GameObject enemy in pool)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }

        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.SetActive(false);
        pool.Add(newEnemy);
        return newEnemy;
    }

    public void ReturnPoolObject(GameObject enemy)
    {
        enemy.SetActive(false);
    }
}
