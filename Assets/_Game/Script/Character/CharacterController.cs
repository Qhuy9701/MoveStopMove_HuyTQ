using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
public GameObject bulletPrefab;
public Transform bulletSpawn;
public float bulletSpeed = 20f;
private bool canShoot = true;
private ObjectPool objectPool;
 private void Awake()
{
    objectPool = FindObjectOfType<ObjectPool>();
}

private void FixedUpdate()
{
    // Check if the character is moving
    if (GetComponent<Rigidbody>().velocity.magnitude > 0f)
    {
        canShoot = true;
    }
    else if (canShoot)
    {
        Collider[] botColliders = Physics.OverlapSphere(transform.position, 2.56f, LayerMask.GetMask("Bot"));
        if (botColliders.Length > 0)
        {
            // Shoot at the first Bot collider found
            Vector3 botPosition = botColliders[0].transform.position;
            Vector3 shootDirection = (botPosition - transform.position).normalized;
            bulletSpawn.LookAt(botPosition);
            Shoot();
        }
        canShoot = false;
    }
}

private IEnumerator DisableBullet(GameObject bullet)
{
    yield return new WaitForSeconds(2f);
    bullet.SetActive(false);
}

private void Shoot()
{
    if (objectPool == null)
    {
        objectPool = FindObjectOfType<ObjectPool>();
        if (objectPool == null)
        {
            Debug.LogError("ObjectPool instance not found.");
            return;
        }
    }

    GameObject bullet = objectPool.SpawnFromPool("Bullet", bulletSpawn.position, bulletSpawn.rotation);
    if (bullet != null)
    {
        bullet.transform.position = bulletSpawn.position;
        bullet.transform.rotation = bulletSpawn.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        StartCoroutine(DisableBullet(bullet));
    }
    else
    {
        Debug.Log("No available bullets in pool.");
    }
}
}