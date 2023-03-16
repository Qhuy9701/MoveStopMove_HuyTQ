using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    [SerializeField] JoyStickMove joyStickMove;
    private WeaponType selectedWeapon;

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
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack(selectedWeapon);
        }

        Move();
    }

    public override void Move()
    {
        // Check if the character is moving
        if (GetComponent<Rigidbody>().velocity.magnitude > 0f)
        {
            _isAttack = true;
        }
        else if (_isAttack)
        {
            Collider[] botColliders = Physics.OverlapSphere(transform.position, _attackRange, LayerMask.GetMask("Bot"));
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
                _attackPoint.LookAt(botPosition);
                Attack(selectedWeapon);
                // Debug
                Debug.DrawLine(transform.position, botPosition, Color.red, 1f);
                Debug.Log("Attack");
            }

            _isAttack = false;
        }
    }

    public void SelectWeapon(WeaponType weapon)
    {
        selectedWeapon = weapon;
    }
}
