using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{

    public float parallaxScale;
    public ParallaxEffect parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        parallaxEffect.SetParallaxEffect(parallaxScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
