using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class Bot : CharacterController
{
    private NavMeshAgent agent;
    private List<GameObject> characters = new List<GameObject>();
    private CharacterSpawner characterSpawner;

    private void Awake()
    {
        base.Awake();
        characterSpawner = FindObjectOfType<CharacterSpawner>();
    }

    private void Start()
    {
        if (characterSpawner != null)
        {
            characters = characterSpawner.GetCharacters();
            if (characters.Count > 0)
            {
                _currentTarget = characters[Random.Range(0, characters.Count)].transform;
            }
            else
            {
                Debug.LogError("No characters found in list!");
                return;
            }
        }
        else
        {
            Debug.LogError("CharacterSpawner not found!");
            return;
        }
    }

    private void Update()
    {
        // Kiểm tra khoảng cách giữa Bot và mục tiêu hiện tại
        float distance = Vector3.Distance(transform.position, _currentTarget.position);
        if (distance <= _attackRange)
        {
            // Dừng lại nếu mục tiêu nằm trong range bắn
            agent.isStopped = true;
            // Random lại target sau 0.5 giây
            StartCoroutine(RandomTargetAfterDelay(0.5f));
        }
        else
        {
            // Tiếp tục di chuyển tới target
            agent.isStopped = false;
            agent.SetDestination(_currentTarget.position);
        }
    }

    private IEnumerator RandomTargetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Random lại target mới
        if (characters.Count > 0)
        {
            _currentTarget = characters[Random.Range(0, characters.Count)].transform;
        }
        else
        {
            Debug.LogError("No characters found in list!");
        }
    }
}
