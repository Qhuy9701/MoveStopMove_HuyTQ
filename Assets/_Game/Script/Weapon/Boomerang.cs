using UnityEngine;
using System.Collections;

public class Boomerang : MonoBehaviour
{
    CharacterController _characterController;
    public float speed = 10f;
    public float returnDelay = 1f;
    public float returnSpeed = 5f;

    private Vector3 initialPosition;
    private bool isReturning = false;

    private void OnEnable()
    {
        initialPosition = transform.position;
        isReturning = false;
        StartCoroutine(ReturnToShootPoint());
    }

    public void SetCharacter(CharacterController characterController)
    {
        this._characterController = characterController;
    }

    private void Update()
    {
        if (!isReturning)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _characterController.AttackPoint.position, returnSpeed * Time.deltaTime);
            if (transform.position == _characterController.AttackPoint.position)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator ReturnToShootPoint()
    {
        yield return new WaitForSeconds(returnDelay);
        isReturning = true;
    }
}
