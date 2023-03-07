// using UnityEngine;
// using UnityEngine.AI;
// using System.Collections;
// using System.Collections.Generic;

// public class BotAI : CharacterController
// {
// public GameObject currentTarget;
// private NavMeshAgent agent;
// private bool isAttacking;
// private bool canAttack;
// private void Awake()
// {
//     agent = GetComponent<NavMeshAgent>();
// }

// private void Start()
// {
//     currentTarget = GetNewTarget();
//     agent.SetDestination(currentTarget.transform.position);
// }


// private void Update()
// {
//     if (!isDead)
//     {
//         if (currentTarget == null || agent.remainingDistance <= agent.stoppingDistance)
//         {
//             currentTarget = GetNewTarget();
//             agent.SetDestination(currentTarget.transform.position);
//             isAttacking = false;
//         }

//         if (!isAttacking && agent.remainingDistance > agent.stoppingDistance)
//         {
//             agent.isStopped = false;
//         }

//         if (!isAttacking && agent.remainingDistance <= agent.stoppingDistance)
//         {
//             canAttack = true;
//             agent.isStopped = true;
//             agent.velocity = Vector3.zero;

//             if (currentTarget.CompareTag("Character"))
//             {
//                 BotAI targetBot = currentTarget.GetComponent<BotAI>();
//                 if (targetBot != null && targetBot.isDead)
//                 {
//                     currentTarget = GetNewTarget();
//                     agent.SetDestination(currentTarget.transform.position);
//                     isAttacking = false;
//                 }
//                 else
//                 {
//                     StartCoroutine(Attack());
//                 }
//             }
//             else
//             {
//                 StartCoroutine(Attack());
//             }
//         }
//     }
// }


// private IEnumerator Attack()
// {
//     yield return new WaitForSeconds(2f);
//     currentTarget = GetNewTarget();

//     if (currentTarget.CompareTag("Character"))
//     {
//         BotAI targetBot = currentTarget.GetComponent<BotAI>();
//         if (targetBot != null && !targetBot.isDead)
//         {
//             agent.isStopped = false;
//             agent.SetDestination(currentTarget.transform.position);
//             isAttacking = false;
//         }
//         else
//         {
//             currentTarget = GetNewTarget();
//             agent.SetDestination(currentTarget.transform.position);
//             isAttacking = false;
//         }
//     }
//     else
//     {
//         currentTarget = GetNewTarget();
//         agent.SetDestination(currentTarget.transform.position);
//         isAttacking = false;
//     }
// }

// private GameObject GetNewTarget()
// {
//     List<GameObject> targets = GameManager.Instance.GetCharacters();

//     // Remove this bot from the list of targets
//     targets.Remove(gameObject);

//     GameObject newTarget = targets[Random.Range(0, targets.Count)];

//     if (newTarget == gameObject)
//     {
//         return GetNewTarget();
//     }

//     return newTarget;
// }

// public void TakeDamage()
// {
//     isDead = true;
//     agent.isStopped = true;
//     agent.velocity = Vector3.zero;
// }
// }