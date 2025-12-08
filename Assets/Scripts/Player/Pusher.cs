using UnityEngine;

public class Pusher : MonoBehaviour
{
    Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Movement movement))
        {
            //Debug.Log("Movement");
            Vector3 knockbackDirection = player.Movement.MoveDirection;

            //rb.linearVelocity = Vector3.zero;

            movement.Knockback(knockbackDirection * player.KnockbackForce);
        }
        else if (other.TryGetComponent(out Rigidbody rb))
        {
            //Debug.Log("Rigidbody");
            Vector3 knockbackDirection = rb.transform.position - transform.position;
            knockbackDirection.y = 0f;
            knockbackDirection.Normalize();

            //rb.linearVelocity = Vector3.zero;

            rb.AddForce(knockbackDirection * player.KnockbackForce, ForceMode.Impulse);
        }
    }



    public void SetPlayer(Player player)
    {
        this.player = player;
    }
}
