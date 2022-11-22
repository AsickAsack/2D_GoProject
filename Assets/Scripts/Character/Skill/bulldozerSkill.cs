using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulldozerSkill : ConflictAndSKill
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.CompareTag("PlayerBall") || collision.gameObject.CompareTag("EnemyBall"))
        {
            ConflictProcess(collision, 0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            MyRigid.velocity *= 0.7f;
            PlayManager.Instance.objectPool.GetActiveEffects(2, collision.transform.position);
            collision.gameObject.SetActive(false);
        }
    }
}