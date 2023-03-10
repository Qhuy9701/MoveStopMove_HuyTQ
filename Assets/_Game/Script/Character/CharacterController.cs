using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{   
    public SphereCollider attackRange;
    //tranform
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Transform _attackPoint;
    public Transform AttackPoint => _attackPoint;
    [SerializeField] protected Transform _currentTarget;
    //float
    [SerializeField] protected float _speed = 5f;
    [SerializeField] public static float _attackRange = 4f;

    //bool
    [SerializeField] protected bool _isMoving = false;
    [SerializeField] protected bool _isDead = false;
    [SerializeField] protected bool _isAttack = false;

    Bullet bullet;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (_attackPoint == null)
        {
            GameObject attackPointObject = GameObject.FindGameObjectWithTag("AttackPoint");
            if (attackPointObject != null)
            {
                _attackPoint = attackPointObject.transform;
            }
            else
            {
                //_attackPoint = attackPointObject.transform;
                Debug.LogError("Could not find attackpoint object with tag 'attackpoint'.");
            }
        }
    }

    public virtual void OnInit()
    {
        _isMoving = true;
        _isAttack = false;
        _isDead = false;
    }
    public virtual void Attack()
    {
        //GameObject bullet = ObjectPool.Instance.SpawnFromPool(Constants.TAG_BULLET, _attackPoint.position, _attackPoint.rotation);
        GameObject bullet = ObjectPool.Instance.SpawnFromPool(Constants.TAG_BOOMERANG, _attackPoint.position, _attackPoint.rotation);
    }
    public virtual void Move() { }
    public virtual void Die() 
    { 
        _isDead = true;
        gameObject.SetActive(false);
    }

    public virtual void UpSize()
    {
        transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
        _attackRange += 0.5f;

    }    
}
