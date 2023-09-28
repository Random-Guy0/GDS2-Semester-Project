using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public delegate void EnterPortalHandler(PlayerMovement player);

    public event EnterPortalHandler OnEnterPortal;

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
}
