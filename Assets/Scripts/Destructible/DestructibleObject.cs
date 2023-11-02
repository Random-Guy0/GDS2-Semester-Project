using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : Health
{
    private SpriteRenderer _spriteRenderer;
    
    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        OnTakeDamage += TakeDamage;
    }

    private void TakeDamage()
    {
        if (_spriteRenderer != null)
        {
            StartCoroutine(FlashRed());
        }
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = new Color(161 / 255f, 49 / 255f, 25 / 255f, 255 / 255f);
    }
}
