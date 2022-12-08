using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Booster : Obstacle
{
    public float Power;
    public float DelayTime;
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall"))
            if (collision.transform.GetComponent<CharacterSkill>().IgnoreObstacle) return;

        Skill(collision);
    }
    */
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall"))
            if (collision.transform.GetComponent<CharacterSkill>().IgnoreObstacle) return;

        Skill(collision);
        /*
        Rigidbody2D myRigid = collision.transform.GetComponent<Rigidbody2D>();
        myRigid.velocity = Vector2.zero;
        myRigid.AddForce(this.transform.position + this.transform.right * Power, ForceMode2D.Impulse);
        */

    }

    public override void Skill(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall")|| collision.transform.CompareTag("EnemyBall"))
        {
            Rigidbody2D myRigid = collision.transform.GetComponent<Rigidbody2D>();
            StartCoroutine(DelaySkill(myRigid));

            /*
            myRigid.velocity = Vector2.zero;
            myRigid.AddForce(this.transform.position + this.transform.right * Power, ForceMode2D.Impulse);
            */
        }
    }

    IEnumerator DelaySkill(Rigidbody2D rigid)
    {
        yield return new WaitForSeconds(DelayTime);

        rigid.velocity = Vector2.zero;
        rigid.AddForce(this.transform.position + this.transform.right * Power, ForceMode2D.Impulse);

    }

    public override void Skill(Collision2D collision)
    {
       //
    }
}
