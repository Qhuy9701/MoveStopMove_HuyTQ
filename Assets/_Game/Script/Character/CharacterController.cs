using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 20f;
    public bool isMoving = false;
    private bool canShoot = true;

    private void FixedUpdate()
    {
        
        // Check if the character is moving
        if (GetComponent<Rigidbody>().velocity.magnitude > 0f)
        {
            Debug.Log("Velocity > 0");
            isMoving = true;
            canShoot = true;
        }
        else if (isMoving && canShoot)
        {
            Shoot();
            canShoot = false;
            isMoving = false;
        }
    }

    private void Shoot()
    {
        // Tạo đạn
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        // Thiết lập vận tốc cho đạn
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * bulletSpeed;
        Debug.Log("Shoot");
    }

    void OnTriggerEnter(Collider other)
    {
        
    }
}