using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotAI : CharacterController
{
    
    private CharacterSpawner characterSpawner;
    public List<GameObject> characters = new List<GameObject>();
    public NavMeshAgent agent;
    public float distanceThreshold = 0.5f; 
    private int currentTargetIndex = -1;
    private float shootingTimer;
    public Transform target;
    private float timeToMove;

    public void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        characterSpawner = FindObjectOfType<CharacterSpawner>();
        characters = characterSpawner.GetCharacters();
    }

    private void Start()
    {
        if (!agent.isActiveAndEnabled)
        {
            Debug.LogError("NavMeshAgent is not active and enabled!");
        }
        if (!NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 0.1f, NavMesh.AllAreas))
        {
            Debug.LogError("BotController is not on a NavMesh!");
        }

        MoveToRandomTarget();
    }

    private void Update()
    {
        if (_isMoving)
        {
            // check if the bot has reached its destination
            if (agent.remainingDistance < distanceThreshold)
            {
                // wait for a while before moving to a new target
                timeToMove -= Time.deltaTime;
                if (timeToMove <= 0)
                {
                    MoveToRandomTarget();
                    timeToMove = 0.5f;
                }
            }
            else if (target != null && target.gameObject.activeSelf && Vector3.Distance(transform.position, target.position) < _attackRange)
            {
                // stop moving and start shooting if the target is within shooting range
                agent.isStopped = true;
                _isMoving = false;
                _isAttack = true;
            }
        }
        else if (_isAttack)
        {
            if (!agent.isStopped)
            {
                // stop the agent from moving while shooting
                agent.isStopped = true;
            }

            // shoot at the target and wait for the shooting cooldown to end
            transform.LookAt(target.position);
            shootingTimer += Time.deltaTime;
            if (shootingTimer >= 0.5f)
            {
                shootingTimer = 0f;
                Shoot();
                _isAttack = false;
                agent.isStopped = false;
                Invoke("MoveToRandomTarget", 2.5f);
            }
        }
    }

    private void MoveToRandomTarget()
    {
        if (characters.Count == 0) return; // kiểm tra nếu danh sách rỗng

        int newTargetIndex;
        do
        {
            newTargetIndex = Random.Range(0, characters.Count);
        }
        while (newTargetIndex == currentTargetIndex || characters[newTargetIndex] == gameObject);

        currentTargetIndex = newTargetIndex;

        // kiểm tra nếu index hợp lệ trước khi truy cập danh sách
        if (currentTargetIndex >= 0 && currentTargetIndex < characters.Count)
        {
            target = characters[currentTargetIndex].transform;
            agent.SetDestination(target.position);
            _isMoving = true;
        }
    }

    private void Shoot()
    {
        //instant
        GameObject bullet = ObjectPool.Instance.SpawnFromPool(Constants.TAG_BULLET  , _attackPoint.position, _attackPoint.rotation);
        if (bullet != null)
        {
            bullet.transform.position = _attackPoint.position;
            bullet.transform.rotation = _attackPoint.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * _speed;
        }
        else
        {
            Debug.Log("No available bullets in pool.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Vẽ một hình cầu đại diện cho tầm bắn của bot
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

}
