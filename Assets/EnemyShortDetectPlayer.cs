using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShortDetectPlayer : MonoBehaviour
{
   Rigidbody2D rb;
   public GameObject target;
   public float speed = 2.0f;
   private EnemyShortRangedAttack enemyShortRangedAttack;
   public float stopDistance = 6.0f;
   void Start(){
      rb = GetComponent<Rigidbody2D>();
      target = GameManager.Instance.Player;
      enemyShortRangedAttack = GetComponent<EnemyShortRangedAttack>();
   }

   void Update()
   {
      float targetX = target.transform.position.x;
      float desiredX = targetX + stopDistance;

      // Get the screen boundaries
      float leftBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
      float rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
      desiredX = Mathf.Clamp(desiredX, leftBound, rightBound);

      // Move the enemy
      Vector2 newPosition = new Vector2(desiredX, target.transform.position.y);
      transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
   }
}
