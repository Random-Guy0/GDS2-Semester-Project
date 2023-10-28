using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate<T> : DestructibleObject where T : MonoBehaviour
{
    [SerializeField] private float dropRatePercentage;

    [SerializeField] private T drop;

    protected override void Start()
    {
        base.Start();
        dropRatePercentage = dropRatePercentage * 0.01f;
    }

    protected override void Die()
    {
        SpawnDrop();
        Destroy(gameObject);
    }

    protected T SpawnDrop()
    {
        if (Random.value < dropRatePercentage)
        {
            return Instantiate(drop, transform.position, Quaternion.identity);
        }

        return null;
    }
}
