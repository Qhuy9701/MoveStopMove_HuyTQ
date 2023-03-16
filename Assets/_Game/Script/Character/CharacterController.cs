using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public enum WeaponType
{
    Bullet,
    Boomerang
}

public class CharacterController : MonoBehaviour
{   
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

    public virtual void Attack(WeaponType weaponType)
    {
        if (weaponType == WeaponType.Bullet)
        {
            GameObject bullet = ObjectPool.Instance.SpawnFromPool(Constants.TAG_BULLET, _attackPoint.position, _attackPoint.rotation);
            if (bullet != null)
            {
                bullet.transform.position = _attackPoint.position;
                bullet.transform.rotation = _attackPoint.rotation;
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * _speed;
            }
            else
            {
                Debug.Log("No available bullets in pool.");
            }
        }
        else if (weaponType == WeaponType.Boomerang)
        {
            GameObject boomerang = ObjectPool.Instance.SpawnFromPool(Constants.TAG_BOOMERANG, _attackPoint.position, _attackPoint.rotation);
            if (boomerang != null)
            {
                boomerang.transform.position = _attackPoint.position;
                boomerang.transform.rotation = _attackPoint.rotation;
                boomerang.SetActive(true);
                boomerang.GetComponent<Boomerang>().SetCharacter(this);
            }
            else
            {
                Debug.Log("No available boomerangs in pool.");
            }
        }
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
