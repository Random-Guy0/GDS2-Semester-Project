using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public delegate void EnterPortalHandler();

    public event EnterPortalHandler OnEnterPortal;

    protected virtual void EnterPortal()
    {
        OnEnterPortal?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            EnterPortal();
        }
    }
}
