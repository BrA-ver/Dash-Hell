using System;
using UnityEngine;
using UnityEngine.UI;

public class MyCursor : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] Image image;
    [SerializeField] Sprite cursorSprite;

    private void Start()
    {
        SetUmageSprite(cursorSprite);
    }

    private void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        image.transform.position = InputHandler.Instance.MouseInput;
    }

    void SetUmageSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
