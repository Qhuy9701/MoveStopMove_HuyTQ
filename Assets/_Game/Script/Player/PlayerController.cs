using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    public JoyStickMove joyStickMove;  // reference to the JoyStickMove script

    public void Start()
    {
        if(joyStickMove == null)
        {
            joyStickMove = FindObjectOfType<JoyStickMove>();
        }
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    private void FixedUpdate()
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
                    // Shoot at the first Bot collider found
                    Vector3 botPosition = botColliders[0].transform.position;
                    Vector3 shootDirection = (botPosition - transform.position).normalized;
                    attackPoint.LookAt(botPosition);
                    Shoot();
                }
                isAttack = false;
            }
        }

}

