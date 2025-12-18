using System;
using UnityEngine;

public class Enemy : Character
{
    protected Player player;

    [SerializeField] protected float rotationSpeed = 10f;

    protected override void Start()
    {
        base.Start();
        player = FindFirstObjectByType<Player>();
        health.OnDied.AddListener(OnDied);
    }

    private void OnDestroy()
    {
        health.OnDied.RemoveListener(OnDied);
    }

    protected override void Update()
    {
        base.Update();

        HandleMovement();
    }

    protected virtual void HandleMovement()
    {
        bool playerDied = GameManager.Instance.PlayerDied;
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

    protected void LookAt(Vector3 point)
    {
        Vector3 hieghtCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
        Vector3 lookDir = hieghtCorrectedPoint - transform.position;
        lookDir.Normalize();

        Quaternion lookRotation = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    #region Health Events
    protected virtual void OnDied()
    {
        Destroy(gameObject);
    }
    #endregion
}
