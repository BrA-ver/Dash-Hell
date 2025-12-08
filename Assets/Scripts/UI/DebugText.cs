using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DebugText : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] TextMeshProUGUI text;

    bool playerDied;

    private void Start()
    {

        GameEvents.Instance.OnPlayerDied.AddListener(OnPlayerDied);
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnPlayerDied.RemoveListener(OnPlayerDied);
    }

    

    private void Update()
    {
        if (playerDied) return;

        Vector3 speed = rb.linearVelocity;
        speed.y = 0f;
        text.text = $"Speed: {speed.magnitude.ToString("F2")}";
    }

    float RoundToDecimals(float num,int decimalPlaces)
    {
        float roundedFloat = (Mathf.Round(num * 100)) / 100f;
        return roundedFloat;
    }

    private void OnPlayerDied()
    {
        playerDied = true;
    }
}
