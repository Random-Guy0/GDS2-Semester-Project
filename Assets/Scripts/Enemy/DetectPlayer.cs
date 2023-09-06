using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;       

    private Transform cameraTransform;  
    private bool isPlayerVisible = false;
    private Camera mainCamera;
    GameObject playerObject = GameObject.FindGameObjectWithTag("player");
    public Transform player;            


    void Start()
    {
        player = playerObject.transform;
        // Find the main camera in the scene
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            cameraTransform = mainCamera.transform;
        }
        else
        {
            Debug.LogError("Main Camera not found in the scene!");
        }
    }

    void Update()
    {
        // Check if the player is within the camera's view
        if (IsPlayerVisible())
        {
            Console.WriteLine("Visible");
            isPlayerVisible = true;
        }
        else
        {
             Console.WriteLine("Invisible");
            isPlayerVisible = false;
        }

        // If the player is visible, move towards the player
        if (isPlayerVisible)
        {
            MoveTowardsPlayer();
        }
    }

    bool IsPlayerVisible()
    {
        if (cameraTransform == null || player == null)
        {
            return false;
        }

        // Calculate the viewport position of the player
        Vector3 playerViewportPosition = mainCamera.WorldToViewportPoint(player.position);

        // Check if the player is within the camera's viewport
        if (playerViewportPosition.x >= 0f && playerViewportPosition.x <= 1f &&
            playerViewportPosition.y >= 0f && playerViewportPosition.y <= 1f &&
            playerViewportPosition.z > 0f)
        {
            return true;
        }

        return false;
    }

    void MoveTowardsPlayer()
    {
        // Calculate the direction to move towards the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move the enemy towards the player
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}