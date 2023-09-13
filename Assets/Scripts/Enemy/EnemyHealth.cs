using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private SpriteRenderer _spriteRenderer;

    protected override void Start(){
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //bubble function
    protected override void Die(){
        _spriteRenderer.color = Color.blue;
        if (TryGetComponent<BubbledEnemy>(out BubbledEnemy bubbledEnemy))
        {
            bubbledEnemy.enabled = true;
        }
        Destroy(this);
    }
    

}
