using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootForce = 10f;
    bool canShoot = true;

    [SerializeField] float fireRate = .1f;
    float shootCounter;

    [Header("Burst")]
    [SerializeField, Range(1, 25)] int bulletsPerBurst = 3;
    [SerializeField] float burstRate = .25f;

    [Header("Projectile Settings")]
    public int numberOfProjectiles;
    public Rigidbody projectilePrefab;
    public float spreadAngle = 45f;


    private void Update()
    {
        if (!canShoot)
        {
            shootCounter -= Time.deltaTime;
            if (shootCounter <= 0f)
            {
                shootCounter = Random.Range(fireRate * .9f, fireRate * 1.2f);
                canShoot = true;
            }
        }

        Shoot();
    }

    public void Shoot()
    {
        if (!canShoot) return;

        //Rigidbody newBullet = Instantiate(rb, shootPoint.position, Quaternion.identity);
        //newBullet.AddForce(transform.forward * shootForce, ForceMode.Impulse);

        StartCoroutine(ShootRoutine());

        canShoot = false;
    }

    IEnumerator ShootRoutine()
    {
        for (int i = 0; i < bulletsPerBurst; i++)
        {
            SpawnBullet(transform.forward);
            if (bulletsPerBurst > 1)
            {
                yield return new WaitForSeconds(burstRate);
            }
        }
    }

    private void SpawnBullet(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angleStep = spreadAngle / numberOfProjectiles;

        float centerOffset = (spreadAngle / 2) - (angleStep / 2);

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float currentAngle = angleStep * i;
            float angle = targetAngle - currentAngle + centerOffset;
            Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
            Rigidbody newBullet = Instantiate(projectilePrefab, shootPoint.position, rotation);
            newBullet.AddForce(newBullet.transform.forward * shootForce, ForceMode.Impulse);
            //newBullet.transform.right = direction;
        }
    }
}
