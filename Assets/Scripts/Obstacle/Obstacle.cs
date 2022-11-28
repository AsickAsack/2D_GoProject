using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public abstract void Skill(Collision2D collision);
    public abstract void Skill(Collider2D collision);


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall"))
            if (collision.transform.GetComponent<ConflictAndSKill>().IgnoreObstacle) return;

        Skill(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall"))
            if (collision.transform.GetComponent<ConflictAndSKill>().IgnoreObstacle) return;

        
        Skill(collision);
        
    }

    

}
