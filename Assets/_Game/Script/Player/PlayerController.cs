using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    [SerializeField] JoyStickMove joyStickMove;
    [SerializeField] private bool win;

    public void Start()
    {
        if (joyStickMove == null)
        {
            joyStickMove = FindObjectOfType<JoyStickMove>();
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        win = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        // Check if the character is moving
        if (GetComponent<Rigidbody>().velocity.magnitude > 0f)
        {
            isAttack = true;
        }
        else if (isAttack)
        {
            Collider[] botColliders = Physics.OverlapSphere(transform.position, attackRange, LayerMask.GetMask("Bot"));
            if (botColliders.Length > 0)
            {
                float minDistance = Mathf.Infinity;
                Vector3 botPosition = Vector3.zero;

                // Find the closest Bot collider
                foreach (Collider botCollider in botColliders)
                {
                    float distance = Vector3.Distance(transform.position, botCollider.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        botPosition = botCollider.transform.position;
                    }
                }

                // Shoot at the closest Bot collider found
                Vector3 shootDirection = (botPosition - transform.position).normalized;
                attackPoint.LookAt(botPosition);
                Shoot();
            }

            isAttack = false;
        }
    }

}

