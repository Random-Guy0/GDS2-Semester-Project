using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public float maxHealth = 100.0f;
    public float currentHealth;

    void Start(){
        currentHealth = maxHealth;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }        

    void Update(){
    }

    public void TakeDamage(float damage){
        currentHealth -= damage;

        //turn enemy into bubble
        if(currentHealth <= 0){
            Bubble();
        }
    }

    //bubble function
    public void Bubble(){
        _spriteRenderer.color = Color.blue;

    }
    

}
