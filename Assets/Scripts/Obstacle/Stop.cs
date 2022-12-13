using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : Obstacle
{

    public int StopCount;
    Vector2 MinusVector;

    private void Awake()
    {
        MinusVector = new Vector2(10f, 10f);
    }

    public override void Skill(Collision2D collision)
    {
       //
    }

    public override void Skill(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerBall") || collision.transform.CompareTag("EnemyBall"))
        {
            Rigidbody2D TempRigid = collision.GetComponent<Rigidbody2D>();
            StartCoroutine(StopRoutine(TempRigid));
            

        }
    }

    IEnumerator StopRoutine(Rigidbody2D rigidbody)
    {
        //Vector2 temp = rigidbody.velocity * 0.9f;

        for(int i=0;i<10;i++)
        {

            rigidbody.velocity *= 0.8f;

            yield return new WaitForSeconds(0.01f);
        }
    }
}
