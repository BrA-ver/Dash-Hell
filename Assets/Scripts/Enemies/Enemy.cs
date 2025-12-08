using System;
using UnityEngine;

public class Enemy : Character
{
    Player player;
    bool playerDied;

    [SerializeField] float rotationSpeed = 10f;

    protected override void Start()
    {
        base.Start();
        player = FindFirstObjectByType<Player>();

        GameEvents.Instance.OnPlayerDied.AddListener(OnPlayerDied);
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnPlayerDied.RemoveListener(OnPlayerDied);
    }

    protected override void Update()
    {
        base.Update();

        if (playerDied)
        {
            // Stop Moving
            return;
        }

        Vector3 moveDirection = player.transform.position - transform.position;
        moveDirection.y = 0f;
        moveDirection.Normalize();

        movement.Move(moveDirection);

        LookAt(player.transform.position);
    }

    private void LookAt(Vector3 point)
    {
        Vector3 hieghtCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
        Vector3 lookDir = hieghtCorrectedPoint - transform.position;
        lookDir.Normalize();

        Quaternion lookRotation = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnPlayerDied()
    {
        playerDied = true;
    }
}
