using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 2f;
    CharacterSpawner characterSpawner;
    
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
        if (other.CompareTag("Character"))
        {
            ObjectPool.Instance.ReturnToPool("Bot", other.gameObject);
            gameObject.SetActive(false);
            Debug.Log("Hit");
            Invoke("Spawn", 1f);
            characterSpawner.idOfbot--;
        }
    }
    private void Spawn()
    {
        ObjectPool.Instance.SpawnFromPool("Bot", characterSpawner.GetRandomPosition(), Quaternion.identity);
        ObjectPool.Instance.ReturnToPool("Bullet", gameObject);
    }
}
