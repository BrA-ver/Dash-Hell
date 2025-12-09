using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    [SerializeField] bool shake;
    [SerializeField] float shakeStrength = 1f;
    [SerializeField] float shakeTime = .25f;
    float shakeCounter = 0f;
    Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.localPosition;
        GameEvents.Instance.OnPlayerTookDamage.AddListener(OnTookDamage);
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnPlayerTookDamage.RemoveListener(OnTookDamage);
    }

    // Update is called once per frame
    void Update()
    {
        if (shake)
        {
            Vector3 randomPos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f) * shakeStrength;
            transform.localPosition = startPos + randomPos;

            // Count the shake time
            shakeCounter += Time.deltaTime;
            
            if (shakeCounter >= shakeTime)
            {
                shake = false;
                transform.localPosition = startPos;
                shakeCounter = 0f;
            }
            
        }
    }

    private void OnTookDamage()
    {
        shake = true;
    }
}
