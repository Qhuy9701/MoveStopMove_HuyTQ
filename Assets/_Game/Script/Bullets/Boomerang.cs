using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float returnTime = 1f;

    private Rigidbody rb;
    private Vector3 initialPosition;
    private bool isReturning = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        initialPosition = transform.position;
        StartCoroutine(ReturnAfterLifetime());
    }

    private void FixedUpdate()
    {
        if (!isReturning)
        {
            rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        }
        else
        {
            // Calculate the direction back to the initial position
            Vector3 returnDirection = (initialPosition - transform.position).normalized;
            rb.MovePosition(transform.position + returnDirection * speed * Time.fixedDeltaTime);

            // Check if the boomerang has returned to its initial position
            if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator ReturnAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        isReturning = true;

        // Disable collision detection to prevent the boomerang from colliding with anything while returning
        gameObject.GetComponent<Collider>().enabled = false;

        // Wait for the boomerang to return to its initial position
        yield return new WaitForSeconds(returnTime);

        // Enable collision detection and reset the boomerang
        gameObject.GetComponent<Collider>().enabled = true;
        transform.position = initialPosition;
        isReturning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            Debug.Log("Hit");
        }
    }
}
