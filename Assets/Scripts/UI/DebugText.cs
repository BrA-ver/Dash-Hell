using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugText : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] TextMeshProUGUI text;

    private void Update()
    {
        Vector3 speed = rb.linearVelocity;
        speed.y = 0f;
        text.text = $"Speed: {speed.magnitude.ToString("F2")}";
    }

    float RoundToDecimals(float num,int decimalPlaces)
    {
        float roundedFloat = (Mathf.Round(num * 100)) / 100f;
        return roundedFloat;
    }
}
