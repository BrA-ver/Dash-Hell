using UnityEngine;

public class KillPlane : MonoBehaviour
{
    [SerializeField] GameObject popParticle;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        Destroy(collision.collider.gameObject);

        if (collision.transform.TryGetComponent(out Health health))
        {
            health.Die();
        }
        else
        {
            Destroy(collision.collider.gameObject);
        }
        Instantiate(popParticle, collision.gameObject.transform.position, Quaternion.identity);
    }
}
