using System;
using FMODUnity;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public delegate void EnterPortalHandler(PlayerMovement player);

    public event EnterPortalHandler OnEnterPortal;

    [SerializeField] private StudioEventEmitter enterPortalSound;
    [SerializeField] private StudioEventEmitter exitPortalSound;

    protected virtual void EnterPortal(PlayerMovement player)
    {
        OnEnterPortal?.Invoke(player);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            EnterPortal(player);
        }
    }

    private void OnBecameVisible()
    {
        enterPortalSound.Play();
    }

    private void OnBecameInvisible()
    {
        exitPortalSound.Play();
    }
}
