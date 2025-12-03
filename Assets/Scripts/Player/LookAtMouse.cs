using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    Vector2 aim;

    [SerializeField] float rotationSpeed = 8f;



    private void Update()
    {
        aim = InputHandler.Instance.MouseInput;
        HandleRotation();
    }

    void HandleRotation()
    {
        if (!InputHandler.Instance.isGamepad)
        {
            Ray ray = Camera.main.ScreenPointToRay(aim);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                LookAt(point);
                Debug.DrawLine(ray.origin, ray.GetPoint(rayDistance));
            }
        }
    }

    private void LookAt(Vector3 point)
    {
        Vector3 hieghtCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
        Vector3 lookDir = hieghtCorrectedPoint - transform.position;
        lookDir.Normalize();

        Quaternion lookRotation = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }
}
