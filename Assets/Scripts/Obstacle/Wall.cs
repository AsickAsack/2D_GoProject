using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Obstacle
{

    Vector2 mypos;
    private void Awake()
    {
        mypos = this.transform.position;
    }
    public override void Skill(Transform tr)
    {
        if (tr.CompareTag("PlayerBall") || tr.CompareTag("EnemyBall"))
        {

            Debug.Log(tr.transform);
            Rigidbody2D TempRigid = tr.GetComponent<Rigidbody2D>();


            TempRigid.velocity = Vector2.Reflect(TempRigid.velocity, this.transform.up);
            Debug.Log(TempRigid.velocity);

            this.transform.position = mypos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Skill(collision.transform);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
        {
            Rigidbody2D tempvelo = collision.transform.GetComponent<Rigidbody2D>();

            tempvelo.AddForce(Vector2.Reflect(tempvelo.velocity, this.transform.up) * 10.0f, ForceMode2D.Impulse);
        }
        */
       Skill(collision.transform);
    }

}
