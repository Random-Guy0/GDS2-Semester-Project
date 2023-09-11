using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    
    private Vector2 moveInput = Vector2.zero;
    
    public float LookDirection { get; private set; }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput.x != 0f)
        {
            LookDirection = moveInput.x;
        }
    }

    private void Update()
    {
        Vector2 velocity = moveInput * speed;
        transform.position += (Vector3)(velocity * Time.deltaTime);
    }
}
