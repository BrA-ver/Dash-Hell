using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] float radius = .2f;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] Vector3 groundCheckOffset;

    public bool OnGround => Physics.CheckSphere(transform.position + groundCheckOffset, radius, groundLayers);
}
