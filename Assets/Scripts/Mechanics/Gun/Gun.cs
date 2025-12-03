using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootForce = 10f;
    bool canShoot = true;

    [SerializeField] float fireRate = .1f;
    float shootCounter;

    private void Update()
    {
        if (!canShoot)
        {
            shootCounter += Time.deltaTime;
            if (shootCounter >= fireRate)
            {
                shootCounter = 0f;
                canShoot = true;
            }
        }

        Shoot();
    }

    public void Shoot()
    {
        if (!canShoot) return;

        Rigidbody newBullet = Instantiate(rb, shootPoint.position, Quaternion.identity);
        newBullet.AddForce(transform.forward * shootForce, ForceMode.Impulse);

        canShoot = false;
    }
}
