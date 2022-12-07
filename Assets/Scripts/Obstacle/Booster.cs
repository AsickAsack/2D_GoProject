using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Booster : Obstacle
{
    public float Power;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall"))
            if (collision.transform.GetComponent<CharacterSkill>().IgnoreObstacle) return;

        Skill(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall"))
            if (collision.transform.GetComponent<CharacterSkill>().IgnoreObstacle) return;

        Skill(collision);
    }

    public override void Skill(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall")|| collision.transform.CompareTag("EnemyBall"))
        {
            Rigidbody2D myRigid = collision.transform.GetComponent<Rigidbody2D>();
            myRigid.velocity = Vector2.zero;
            myRigid.AddForce(this.transform.position + this.transform.right * Power, ForceMode2D.Impulse);
            
        }
    }

    public override void Skill(Collision2D collision)
    {
       //
    }
}
