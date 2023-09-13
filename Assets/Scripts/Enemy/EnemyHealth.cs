using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private SpriteRenderer _spriteRenderer;
    private GruntDetectPlayer detectPlayerComponent;
    

    protected override void Start(){
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        try{
            detectPlayerComponent = GetComponent<GruntDetectPlayer>();
        }
        catch{
            Debug.Log("No component");
        }
    }        

    //bubble function
    protected override void Die(){
        _spriteRenderer.color = Color.blue;
        if (TryGetComponent<BubbledEnemy>(out BubbledEnemy bubbledEnemy))
        {
            bubbledEnemy.enabled = true;
        }
        Destroy(detectPlayerComponent);
        Destroy(this);
    }
    

}
