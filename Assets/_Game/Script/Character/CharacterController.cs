using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{

    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] public static float attackRange = 2.5f;
    [SerializeField] protected bool isMoving = false;
    [SerializeField] protected bool isDead = false;
    [SerializeField] protected bool isAttack = false;


    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (attackPoint == null)
        {
            GameObject attackPointObject = GameObject.FindGameObjectWithTag("AttackPoint");
            if (attackPointObject != null)
            {
                attackPoint = attackPointObject.transform;
            }
            else
            {
                Debug.LogError("Could not find attackpoint object with tag 'attackpoint'.");
            }
        }
    }

    public virtual void Start()
    {

    }
    public virtual void OnInit()
    {
        isMoving = true;
        isAttack = false;
        isDead = false;
    }
    public virtual void Shoot()
    {
        //GameObject bullet = ObjectPool.Instance.SpawnFromPool("Bullet", attackPoint.position, attackPoint.rotation);
        GameObject bullet = ObjectPool.Instance.SpawnFromPool("Boomerang", attackPoint.position, attackPoint.rotation);
        if (bullet != null)
        {
            bullet.transform.position = attackPoint.position;
            bullet.transform.rotation = attackPoint.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speed;

            StartCoroutine(DisableBullet(bullet));
        }
        else
        {
            Debug.Log("No available bullets in pool.");
        }
    }
    public virtual void Move() { }
    public virtual void Die() { }

    private IEnumerator DisableBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(2f);
        bullet.SetActive(false);
    }
}
