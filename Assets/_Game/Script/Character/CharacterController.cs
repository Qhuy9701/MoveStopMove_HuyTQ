using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Reference to the bullet prefab and spawn point transform
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    // Speed of the bullet
    public float bulletSpeed = 20f;

    // Flag to check if the player can shoot
    private bool canShoot = true;

    // FixedUpdate is called once per physics update
    private void FixedUpdate()
    {
        // Check if the character is not moving and can shoot
        if (GetComponent<Rigidbody>().velocity.magnitude <= 0f && canShoot)
        {
            // Check for bots within the range
            Collider[] botColliders = Physics.OverlapSphere(transform.position, 2.56f, LayerMask.GetMask("Bot"));

            // If a bot is found, shoot at it
            if (botColliders.Length > 0)
            {
                Vector3 botPosition = botColliders[0].transform.position;
                bulletSpawn.LookAt(botPosition);
                Shoot();
            }

            // Mark that the player has shot and cannot shoot again until they move
            canShoot = false;
        }
    }

    // Coroutine to disable bullets after a certain time
    private IEnumerator DisableBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(2f);
        bullet.SetActive(false);
    }

    // Method to shoot a bullet
    private void Shoot()
    {
        GameObject bullet = ObjectPooling.instance.GetPoolObject();

        if (bullet != null)
        {
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

            // Disable the bullet after a certain time
            StartCoroutine(DisableBullet(bullet));
        }
        else
        {
            Debug.Log("No available bullets in pool.");
        }
    }
}
