using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 1f;
    
    private Vector2 moveInput = Vector2.zero;

    private Rigidbody2D rb;
    
    public Vector2 Direction { get; private set; }
    
    public bool CanMove { get; set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput.x != 0f)
        {
            Direction = new Vector2(Mathf.Round(moveInput.x), Mathf.Round(moveInput.y));
            Vector3 scale = transform.localScale;
            scale.x = Direction.x;
            transform.localScale = scale;
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = Vector2.zero;
        
        if(CanMove)
        {
            velocity = moveInput * speed;
        }
        
        rb.velocity = velocity;
        animator.SetFloat("MoveSpeed", velocity.magnitude);
    }

    public void ResetGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.Instance.ResetGame();
        }
    }
}
