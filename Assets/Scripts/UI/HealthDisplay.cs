using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] GameObject[] healthImages;

    private void Start()
    {
        GameEvents.Instance.OnPlayerHealthChanged.AddListener(OnHealthChanged);
    }

    private void OnDisable()
    {
        GameEvents.Instance.OnPlayerHealthChanged.RemoveListener(OnHealthChanged);
        
    }

    private void OnHealthChanged(int currentHealth)
    {
        //for (int i = 0; i < currentHealth; i++)
        //{
        //    if (i >= currentHealth - 1)
        //    {
        //        healthImages[i].SetActive(false);
        //    }
        //    else
        //    {
        //        healthImages[i].SetActive(true);
        //    }
        //}

        for (int i = 0; i < healthImages.Length; i++)
        {
            if (i >= currentHealth)
            {
                healthImages[i].SetActive(false);
            }
            else
            {
                healthImages[i].SetActive(true);
            }
        }
    }
}
