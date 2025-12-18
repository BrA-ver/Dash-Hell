using UnityEngine;

public class Dasher : Enemy
{
    protected override void HandleMovement()
    {
        // Rotate towards the player's position
        LookAt(player.transform.position);

        // Move forward in whatever direction you're facing
        movement.Move(transform.forward);

    }
}
