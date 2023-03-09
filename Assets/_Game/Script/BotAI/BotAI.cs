using UnityEngine;
using System.Collections.Generic;

public class BotAI : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    public GameObject currentTarget;

    private void Start()
    {
        CharacterSpawner spawner = FindObjectOfType<CharacterSpawner>();
        targets = spawner.GetCharacters();
    }

    private void Update()
    {
        if (currentTarget == null)
        {
            currentTarget = GetRandomTarget();
        }

        if (currentTarget == gameObject)
        {
            currentTarget = GetRandomTarget();
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTarget.transform.position) < 0.1f)
        {
            currentTarget = GetRandomTarget();
        }
    }

    private GameObject GetRandomTarget()
    {
        int randomIndex = Random.Range(0, targets.Count);
        GameObject target = targets[randomIndex];
        if (target == gameObject)
        {
            target = GetRandomTarget();
        }
        return target;
    }
}
