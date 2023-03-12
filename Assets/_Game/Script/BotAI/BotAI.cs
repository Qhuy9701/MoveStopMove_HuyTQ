using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotAI : CharacterController
{
    private NavMeshAgent agent;
    public List<GameObject> characters = new List<GameObject>();
    private bool _hasFired = false; 

    
    void Start()
    {
        _currentTarget = characters[Random.Range(0, characters.Count)].transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = _speed;
        agent.stoppingDistance = _attackRange;
        agent.SetDestination(_currentTarget.position);
    }
    void Update()
    {
        Move();
    }

    public override void Move()
    {
        if (_isDead)
        {
            return;
        }

        if (_currentTarget != null)
        {
            // Tính toán khoảng cách giữa bot và current target
            float distanceToTarget = Vector3.Distance(transform.position, _currentTarget.transform.position);

            // Nếu current target trong tầm bắn
            if (distanceToTarget <= _attackRange)
            {
                StopMove();
                return;
            }

            // Di chuyển tới current target
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget.transform.position, _speed * Time.deltaTime);

            // Nếu đang chạy thì dừng chạy
            if (_isMoving)
            {
                _isMoving = false;
            }
        }
    }

    public void StopMove()
    {
        if (_isDead)
        {
            return;
        }

        // Dừng di chuyển
        _isMoving = false;

        // Kiểm tra nếu đang tấn công thì dừng tấn công
        if (_isAttack)
        {
            _isAttack = false;
        }

        // Bắn đạn
        if (!_hasFired)
        {
            Attack();
        }
    }

    private IEnumerator ResetFire()
    {
        yield return new WaitForSeconds(1f);
        _hasFired = false;
    }   
}
