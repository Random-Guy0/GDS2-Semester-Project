using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGscroll : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bgImg;
    [SerializeField] private float speedX = 0.2f;
    [SerializeField] private float speedY = 0.0f;

    void Update()
    {
        // X & Y
        Vector2 offset = new Vector2(Time.deltaTime * speedX, 0);

        bgImg.material.mainTextureOffset = offset;
        //bgImg.uvRect = new Rect(bgImg.uvRect.position + new Vector2(x, y) * Time.deltaTime, bgImg.uvRect.size);
    }
}
