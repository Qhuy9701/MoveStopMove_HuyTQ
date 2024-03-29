using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 2f;
    CharacterSpawner characterSpawner;
    CharacterController characterController;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        characterSpawner = FindObjectOfType<CharacterSpawner>();
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyAfterLifetime());
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
        {
            ObjectPool.Instance.ReturnToPool(Constants.TAG_BOT, other.gameObject);
            Debug.Log("Hit");

            // Lấy tham chiếu đến đối tượng CharacterController
            characterController = other.GetComponent<CharacterController>();
            characterController.Die();

            // Gán lại giá trị mới cho characterSpawner
            characterSpawner = FindObjectOfType<CharacterSpawner>();
            
            Invoke("Spawn", 1f);
        }
    }


    private void Spawn()
    {
        if (characterSpawner != null)
        {
            ObjectPool.Instance.SpawnFromPool(Constants.TAG_BOT, characterSpawner.GetRandomPosition(), Quaternion.identity);
        }
    }

}
