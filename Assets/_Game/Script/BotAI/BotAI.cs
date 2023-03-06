using UnityEngine;
using UnityEngine.AI;

public class BotAI : MonoBehaviour
{
    //navmesh agent and target player
    private NavMeshAgent agent;
    [SerializeField] private Transform target;

    [SerializeField] private float speed = 10f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

}
