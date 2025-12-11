using UnityEngine;

public class KillPlane : MonoBehaviour
{
    [SerializeField] GameObject popParticle;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.TryGetComponent(out Health health))
        {
            health.Die();
        }
        else
        {
            Destroy(collision.collider.gameObject);
            Instantiate(popParticle, collision.gameObject.transform.position, Quaternion.identity);
        }
        
    }
}
