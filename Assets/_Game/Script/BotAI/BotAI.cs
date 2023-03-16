using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class BotAI : CharacterController
{
   private NavMeshAgent agent;
   public List<GameObject> characters = new List<GameObject>();
   private CharacterSpawner characterSpawner;
   private bool _hasFired = false;

   private void Awake()
   {
       base.Awake();
       characterSpawner = FindObjectOfType<CharacterSpawner>();

   }
   void Start()
   {
       CharacterSpawner characterSpawner = FindObjectOfType<CharacterSpawner>();
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

       agent = GetComponent<NavMeshAgent>();
       agent.speed = _speed;
       agent.stoppingDistance = _attackRange;
       agent.SetDestination(_currentTarget.position);
   }

   // ...

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
           Attack(selectedWeapon);
       }
   }

   private IEnumerator ResetFire()
   {
       yield return new WaitForSeconds(1f);
       _hasFired = false;
   }


    public override void SelectWeapon(WeaponType weapon)
    {
        
    }
}
