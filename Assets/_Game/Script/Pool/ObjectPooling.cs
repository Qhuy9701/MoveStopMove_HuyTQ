using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;

    private List<GameObject> pools = new List<GameObject>();
    private int amounttoPools = 20;

    [SerializeField] private GameObject bulletPrefabs;
    //[SerializeField] private GameObject enemyPrefabs;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < amounttoPools; i++)
        {
            GameObject obj = Instantiate(bulletPrefabs);
            obj.SetActive(false);
            pools.Add(obj);
        }
    }

    public GameObject GetPoolObject()
    {
        for (int i = 0; i < pools.Count; i++)
        {
            if (!pools[i].activeInHierarchy)
            {
                return pools[i];
            }
        }
        return null;
    }

}