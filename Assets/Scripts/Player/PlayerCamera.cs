using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    Player player;

    [SerializeField] float smoothTime;
    Vector3 refVelocity = Vector3.zero;

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (!player) return;

        //transform.position = player.transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref refVelocity, smoothTime);
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }
}
