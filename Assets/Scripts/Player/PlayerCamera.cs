using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    Player player;

    [SerializeField] float smoothTime;
    Vector3 refVelocity = Vector3.zero;

    Vector3 mousePos;

    [SerializeField] Transform offsetTarget;
    [SerializeField] Camera cam;
    [SerializeField] float offsetMagnitude = 2f;
    [SerializeField] float aimThreshold = 2f;
    Vector3 startLocalPos;

    [SerializeField] float aimSmoothTime;
    [SerializeField] float resetSmoothTime;
    Vector3 aimRefVelocity = Vector3.zero;
    bool resetOffset;

    private void Start()
    {
        startLocalPos = offsetTarget.transform.localPosition;
    }

    private void FixedUpdate()
    {
        FollowPlayer();
        //HandleAimOffset();
        if (resetOffset)
        {
            offsetTarget.localPosition = Vector3.SmoothDamp(offsetTarget.localPosition, startLocalPos, ref aimRefVelocity, resetSmoothTime);
            cam.transform.position = offsetTarget.position;
        }
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

    public void GetMousePoint(Vector3 pos)
    {
        mousePos = pos;
    }

    public void HandleAimOffset()
    {
        resetOffset = false;

        // Get the offset of the mouse position
        Vector3 offset = mousePos - transform.position;
        offset.y = 0f;

        if (offset.magnitude > aimThreshold)
        {

            offset = Vector3.ClampMagnitude(offset, offsetMagnitude);

            //offsetTarget.transform.localPosition = startLocalPos + offset;
            offsetTarget.localPosition = Vector3.SmoothDamp(offsetTarget.localPosition, startLocalPos + offset, ref aimRefVelocity, aimSmoothTime);
        }
        else
        {
            //offsetTarget.transform.localPosition = startLocalPos;
            offsetTarget.localPosition = Vector3.SmoothDamp(offsetTarget.localPosition, startLocalPos, ref aimRefVelocity, aimSmoothTime);
        }

        cam.transform.position = offsetTarget.position;
    }

    public void ResetOffset()
    {
        resetOffset = true;
    }
}
