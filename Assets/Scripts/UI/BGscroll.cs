using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGscroll : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bgImg;
    [SerializeField] private float x, y;

    void Update()
    {
        //bgImg.uvRect = new Rect(bgImg.uvRect.position + new Vector2(x, y) * Time.deltaTime, bgImg.uvRect.size);
    }
}
