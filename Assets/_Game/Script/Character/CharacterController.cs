    using System.Collections.Generic;
    using UnityEngine;
    using System.Collections;

    public class CharacterController : MonoBehaviour
    {
        [SerializeField] protected Rigidbody rb;
        [SerializeField] protected float speed = 2f;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject boomerangPrefab;
        [SerializeField] protected Transform attackPoint;
        [SerializeField] public static float attackRange = 5f;
        [SerializeField] protected bool isMoving = false;
        [SerializeField] protected bool isDead = false ;
        [SerializeField] protected bool isAttack = false;

        public virtual void OnInit()
        {
            rb = GetComponent<Rigidbody>();
            isMoving = true;
            isAttack = false;
            isDead = false;
        }
        public virtual void Shoot()
        {
            GameObject bullet = ObjectPool.Instance.SpawnFromPool("Bullet", attackPoint.position, attackPoint.rotation);
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
        public virtual void Move(){}
        public virtual void Die(){}
        
        private IEnumerator DisableBullet(GameObject bullet)
        {
            yield return new WaitForSeconds(2f);
            bullet.SetActive(false);
        } 
    }
