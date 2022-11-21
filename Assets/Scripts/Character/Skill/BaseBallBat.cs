using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBallBat : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("EnemyBall"))
        {
            Debug.Log(collision.transform.name);
            
            MonsterPlay Enemy = collision.GetComponent<MonsterPlay>();

            Enemy.GoForward(collision.transform.position-this.transform.position.normalized,20.0f);

        }

        if (collision.transform.CompareTag("PlayerBall"))
        {
            ConflictAndSKill Enemy = collision.GetComponent<ConflictAndSKill>();

            Enemy.GoForward(collision.transform.position - this.transform.position.normalized, 20.0f);

        }

    }

}
