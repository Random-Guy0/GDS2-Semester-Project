using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongDetectPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
   public GameObject target;
   public float speed = 2.0f;
   private EnemyLongRangedAttack enemyLongRangedAttack;
   void Start(){
      rb = GetComponent<Rigidbody2D>();
      target = GameManager.Instance.Player;
      enemyLongRangedAttack = GetComponent<EnemyLongRangedAttack>();
   }

   void Update()
   {
      float targetY = target.transform.position.y;
      Vector2 newPosition = new Vector2(transform.position.x, target.transform.position.y);
      
      transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
   }
}
