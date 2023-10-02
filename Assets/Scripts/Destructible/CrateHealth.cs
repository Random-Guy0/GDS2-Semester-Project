using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateHealth : Health
{
    private SpriteRenderer _spriteRenderer;
    public float dropRatePercentage;

    [SerializeField] private HealthPickup pickup;
    [SerializeReference] private int healthAmount = 1;

    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        OnTakeDamage += TakeDamage;
        dropRatePercentage = dropRatePercentage / 100f;
    }

    private void TakeDamage()
    {
        StartCoroutine(FlashRed());
    }

    protected override void Die()
    {
        if (Random.value < dropRatePercentage)
        {
            HealthPickup newPickup = Instantiate(pickup, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = new Color(161 / 255f, 49 / 255f, 25 / 255f, 255 / 255f);
    }
}
