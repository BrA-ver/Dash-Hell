using System;
using UnityEngine;

public class RadialBulletController : MonoBehaviour
{
    [Header("Projectile Settings")]
    public int numberOfProjectiles;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public float spreadAngle = 45f;

    Vector3 startPos;
    const float radius = 1f;

    private void Start()
    {
        InputHandler.Instance.onDashPressed += Shoot;
    }

    private void OnDisable()
    {
        InputHandler.Instance.onDashPressed -= Shoot;
    }

    private void Shoot()
    {
        startPos = transform.position;
        //SpawnProjectile(numberOfProjectiles);
        SpawnBullet(transform.forward);
    }

    private void SpawnProjectile(int _numberOfProjectiles)
    {
        float angleStep = spreadAngle / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {
            float projectileDirXPosition = startPos.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPos.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0f);
            Vector3 projectileMoveDir = (projectileVector - startPos).normalized * projectileSpeed;

            GameObject tmObj = Instantiate(projectilePrefab, startPos, Quaternion.identity);
            tmObj.GetComponent<Rigidbody>().linearVelocity = new Vector3(projectileMoveDir.x, 0f, projectileMoveDir.y);

            angle += angleStep;
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
            GameObject newBullet = Instantiate(projectilePrefab, transform.position, rotation);
            newBullet.GetComponent<Rigidbody>().linearVelocity = newBullet.transform.forward * projectileSpeed;
            //newBullet.transform.right = direction;
        }
    }
}
